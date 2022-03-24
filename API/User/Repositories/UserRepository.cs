using API.User.DTOs;
using API.User.Models;
using Microsoft.AspNetCore.Identity;

namespace API.User.Repositories
{
    /// <summary>
    /// User repository class for user services, implements the IUserRepository interface.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> userManager;

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

        public async Task<UserDTO?> FindByEmailAsync(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null || !user.EmailConfirmed)
            {
                return null;
            }

            return new UserDTO(user);
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO userDTO)
        {
            var user = await userManager.FindByIdAsync(userDTO.Id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NotFound",
                    Description = "There is no user in this University with this Id."
                });
            }

            user.FirstName = userDTO.FirstName.Trim();
            user.LastName = userDTO.LastName.Trim();

            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(string userEmail, string userPassword)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NotFound",
                    Description = "There is no user in this University with this Email."
                });
            }

            return await userManager.DeleteAsync(user);
        }

        public async Task<UserModel?> FindUserByIdAsync(string userId) => await userManager.FindByIdAsync(userId);
    }
}
