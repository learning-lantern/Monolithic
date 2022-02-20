using APIs.Data.DTOs;
using Microsoft.AspNetCore.Identity;

namespace APIs.Repositories.UserRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
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
