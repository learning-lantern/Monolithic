using API.Authentication.DTOs;
using Microsoft.AspNetCore.Identity;

namespace API.Authentication.Repositories
{
    /// <summary>
    /// User repository interface for user repository class to applay the dependency injection.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing the user matching the specified userId if it exists.
        /// </returns>
        public Task<UserDTO?> FindByIdAsync(string userId);

        /// <summary>
        /// Updates the specified user in the backing store.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IdentityResult of the operation.
        /// </returns>
        public Task<IdentityResult> UpdateAsync(UserDTO userDTO);

        /// <summary>
        /// Deletes the specified user from the backing store.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IdentityResult of the operation.
        /// </returns>
        public Task<IdentityResult> DeleteAsync(string userId);
    }
}
