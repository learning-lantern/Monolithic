using Microsoft.AspNetCore.Identity;
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
        public async Task<string?> SignUpAsync(SignUpDTO signUpModel)
        {
            UserModel user = await userManager.FindByEmailAsync(signUpModel.Email);

            if (user is not null)
            {
                return null;
            }

            user = new()
            {
                Email = signUpModel.Email,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Image = signUpModel.Image
            };

            await userManager.CreateAsync(user, signUpModel.Password);

            //SmtpClient client = new("smtp.gmail.com");

            //MailMessage message = new("elmourchiditest@gmail.com", user.Email, "Test Subject", "Test");

            //client.Credentials = new NetworkCredential("elmourchiditest", "12345678Hr@#$");
            //client.Port = 587;
            //await client.SendMailAsync(message);

            return user.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<UserModel> FindByIdAsync(string id) => await userManager.FindByIdAsync(id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<UserModel> FindByEmailAsync(string email) => await userManager.FindByEmailAsync(email);
    }
}
