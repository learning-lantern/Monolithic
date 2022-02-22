using APIs.Data.Auth.DTOs;
using Microsoft.AspNetCore.Identity;

namespace APIs.Repositories.Auth
{
    public interface IAuthRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> CreateAsync(CreateDTO createDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<SignInResponseDTO> SignInAsync(SignInDTO signInDTO);

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
