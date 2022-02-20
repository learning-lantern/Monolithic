using APIs.Data.DTOs;
using APIs.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace APIs.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUpDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<string?> CreateAsync(SignUpDTO signUpDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<string?> SignInAsync(SignInDTO signInDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<string?> SendConfirmationEmailAsync(UserModel userModel);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}
