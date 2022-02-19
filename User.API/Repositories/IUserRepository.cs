using Microsoft.AspNetCore.Identity;
using User.API.Data.DTOs;
using User.API.Data.Models;

namespace User.API.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
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

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<UserDTO?> FindByIdAsync(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> UpdateAsync(UserDTO userDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> DeleteAsync(string userId);
    }
}
