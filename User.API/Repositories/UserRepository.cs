using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using User.API.Data.DTOs;
using User.API.Data.Models;

namespace User.API.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IConfiguration configuration;
        private static readonly Random random = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        public UserRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<string?> CreateAsync(SignUpDTO signUpDTO)
        {
            var user = new UserModel()
            {
                UserName = signUpDTO.Email,
                Email = signUpDTO.Email,
                FirstName = signUpDTO.FirstName,
                LastName = signUpDTO.LastName,
                Image = signUpDTO.Image,
                ConfirmationCode = new string(
                    value: Enumerable.Repeat(element: configuration["Chars"], count: 10).Select(
                    selector => selector[random.Next(maxValue: selector.Length)]).ToArray())
            };

            var createAsyncResult = await userManager.CreateAsync(user: user, password: signUpDTO.Password);

            return createAsyncResult.Succeeded ? await SendConfirmationEmailAsync(userModel: user) : null;
        }

        public async Task<string?> SendConfirmationEmailAsync(UserModel userModel)
        {
            var messageBody = $"<h1>Welcome To Learning Lantern</h1><br><p> Thanks for registering at learning lantern please click <strong><a href=\"https://localhost:5001/api/User/ConfirmEmail?userId={userModel.Id}&confirmationCode={userModel.ConfirmationCode}\" target=\"_blank\">here</a></strong> to activate your account</p>";

            var smtpClient = new SmtpClient(host: configuration["SMTP:Host"], port: 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(userName: configuration["SMTP:Credentials:UserName"], password: configuration["SMTP:Credentials:Password"])
            };

            var mailMessage = new MailMessage(from: configuration["SMTP:Credentials:UserName"], to: userModel.Email, subject: "Confirmation Email", body: messageBody)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(message: mailMessage);

            return userModel.Id;
        }

        public async Task<UserDTO?> FindByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId: userId);

            if (user == null || !user.EmailConfirmed)
            {
                return null;
            }

            return new UserDTO(user);
        }

        public async Task<UserDTO?> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email: email);

            if (user == null || !user.EmailConfirmed)
            {
                return null;
            }

            return new UserDTO(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string confirmationCode)
        {
            var user = await userManager.FindByIdAsync(userId: userId);

            if (user is null || confirmationCode != user.ConfirmationCode)
            {
                return IdentityResult.Failed();
            }

            user.EmailConfirmed = true;

            return await userManager.UpdateAsync(user);
        }

        public async Task<string?> SignInAsync(SignInDTO signInDTO)
        {
            var passwordSignInAsyncResult = await signInManager.PasswordSignInAsync(userName: signInDTO.Email, password: signInDTO.Password, isPersistent: true, lockoutOnFailure: false);

            if (!passwordSignInAsyncResult.Succeeded)
            {
                return null;
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

            return new JwtSecurityTokenHandler().WriteToken(token: token);
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO userDTO)
        {
            var user = await userManager.FindByIdAsync(userDTO.Id);

            if (user is null)
            {
                return IdentityResult.Failed();
            }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Image = userDTO.Image;

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return IdentityResult.Failed();
            }

            return await userManager.DeleteAsync(user: user);
        }
    }
}
