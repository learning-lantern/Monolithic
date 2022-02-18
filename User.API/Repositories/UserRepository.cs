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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUpDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<string?> CreateAsync(SignUpDTO signUpDTO)
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

            return createAsyncResult.Succeeded ? await SendConfirmationEmailAsync(user) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// 
        /// </returns>
        private async Task<string?> SendConfirmationEmailAsync(UserModel userModel)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(userModel);

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var messageBody = $"<h1>Welcome To Learning Lantern</h1><br><p> Thanks for registering at learning lantern please click <strong><a href=\"https://localhost:5001/api/User/ConfirmEmail/{userModel.Id}/{token}/\" target=\"_blank\">here</a></strong> to activate your account</p>";

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(userName: "elmourchiditest@gmail.com", password: "12345678Hr@#$")
            };

            var mailMessage = new MailMessage(from: "elmourchiditest@gmail.com", to: userModel.Email, subject: "Confirm Email", body: messageBody)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(mailMessage);

            return userModel.Id;
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<UserModel?> FindByIdAsync(string id) => await userManager.FindByIdAsync(id);

        /// <summary>
        /// Gets the user, if any, associated with the normalized value of the specified email address. Note: Its recommended that identityOptions.User.RequireUniqueEmail be set to true when using this method, otherwise the store may throw if there are users with duplicate emails.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<UserModel?> FindByEmailAsync(string email) => await userManager.FindByEmailAsync(email);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<IdentityResult> ConfirmEmailAsync(UserModel userModel, string token) => await userManager.ConfirmEmailAsync(userModel, token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDTO"></param>
        /// <returns>
        /// 
        /// </returns>
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

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:IssuerSigningKey"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key: issuerSigningKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<IdentityResult> UpdateAsync(UserModel userModel) => await userManager.UpdateAsync(userModel);
    }
}
