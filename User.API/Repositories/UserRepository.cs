using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public UserRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUpModel"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<string?> CreateAsync(SignUpDTO signUpModel)
        {
            var user = new UserModel()
            {
                UserName = signUpModel.Email,
                Email = signUpModel.Email,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Image = signUpModel.Image
            };

            var createAsyncResult = await userManager.CreateAsync(user, signUpModel.Password);

            if (!createAsyncResult.Succeeded)
            {
                return null;
            }

            return await SendConfirmationEmail(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<string?> SendConfirmationEmail(UserModel user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            if (token is null)
            {
                return null;
            }

            var confirmEmailUri = $"https://learning-lantern.web.app/en/auth/confirmation/{user.Id}/{token}/";

            var client = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(userName: "elmourchiditest@gmail.com", password: "12345678Hr@#$")
            };

            var message = new MailMessage(from: "elmourchiditest@gmail.com", to: user.Email, subject: "Confirm Email", body: confirmEmailUri);

            await client.SendMailAsync(message);

            return user.Id;
        }

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<IdentityResult> ConfirmEmailAsync(UserModel user, string token) => await userManager.ConfirmEmailAsync(user, token);

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
    }
}
