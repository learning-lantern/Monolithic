using APIs.Data.User.DTOs;
using APIs.Data.User.Models;
using Microsoft.AspNetCore.Identity;

namespace APIs.Repositories.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public UserRepository(UserManager<UserModel> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserDTO?> FindByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || !user.EmailConfirmed)
            {
                return null;
            }

            return new UserDTO(user);
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

            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return IdentityResult.Failed();
            }

            return await userManager.DeleteAsync(user);
        }
    }
}
