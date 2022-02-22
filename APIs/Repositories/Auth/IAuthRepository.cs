using APIs.Data.Auth.DTOs;
using Microsoft.AspNetCore.Identity;

namespace APIs.Repositories.Auth
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
        public Task<IdentityResult> CreateAsync(SignUpDTO signUpDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<SignInRO> SignInAsync(SignInDTO signInDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> SendConfirmationEmailAsync(string userEmail);

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
