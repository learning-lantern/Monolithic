using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web;
using APIs.Data.Auth.DTOs;
using APIs.Data.User.Models;

namespace APIs.Repositories.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        public AuthRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> CreateAsync(SignUpDTO signUpDTO)
        {
            var user = new UserModel()
            {
                UserName = signUpDTO.Email,
                Email = signUpDTO.Email,
                FirstName = signUpDTO.FirstName,
                LastName = signUpDTO.LastName,
                Image = signUpDTO.Image
            };

            var createAsyncResult = await userManager.CreateAsync(user, signUpDTO.Password);

            return createAsyncResult.Succeeded ?
                await SendConfirmationEmailAsync(user.Email) : IdentityResult.Failed();
        }

        public async Task<SignInRO> SignInAsync(SignInDTO signInDTO)
        {
            var passwordSignInAsyncResult = await signInManager.PasswordSignInAsync(
                userName: signInDTO.Email, password: signInDTO.Password,
                isPersistent: true, lockoutOnFailure: false);

            if (!passwordSignInAsyncResult.Succeeded)
            {
                return new SignInRO("","", "", null);
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Name, value: signInDTO.Email),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
            };

            var issuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(configuration["JWT:IssuerSigningKey"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key: issuerSigningKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
                );

            var user = await userManager.FindByEmailAsync(signInDTO.Email);

            return new SignInRO(user.Id, user.FirstName, user.LastName,
                new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<IdentityResult> SendConfirmationEmailAsync(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            var token = HttpUtility.UrlEncode(
                await userManager.GenerateEmailConfirmationTokenAsync(user));

            var messageBody = $"<h1>Welcome To Learning Lantern</h1><br><p> Thanks for registering at learning lantern please click <strong><a href=\"https://learning-lantern.web.app/en/auth/email-validation?userId={user.Id}&token={token}\" target=\"_blank\">here</a></strong> to activate your account</p>";

            var smtpClient = new SmtpClient(host: configuration["SMTP:Host"], port: 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    userName: configuration["SMTP:Credentials:UserName"],
                    password: configuration["SMTP:Credentials:Password"])
            };

            var mailMessage = new MailMessage(from: configuration["SMTP:Credentials:UserName"], to: userEmail, subject: "Confirmation Email", body: messageBody)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(mailMessage);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return IdentityResult.Failed();
            }

            return await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
