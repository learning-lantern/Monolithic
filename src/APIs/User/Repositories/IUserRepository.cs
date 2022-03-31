using APIs.User.DTOs;
using APIs.User.Models;
using Microsoft.AspNetCore.Identity;

namespace APIs.User.Repositories
{
    public interface IUserRepository
    {
        public Task<UserDTO?> FindByIdAsync(string userId);

        public Task<UserDTO?> FindByEmailAsync(string userEmail);

        public Task<UserDTO?> UpdateAsync(UserDTO userDTO);

        public Task<IdentityResult> DeleteAsync(string userEmail, string userPassword);

        public Task<UserModel?> FindUserByIdAsync(string userId);
    }
}
