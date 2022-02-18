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
        public Task<string?> CreateAsync(SignUpDTO signUpDTO);
        public Task<UserModel?> FindByIdAsync(string id);
        public Task<UserModel?> FindByEmailAsync(string email);
        public Task<IdentityResult> ConfirmEmailAsync(UserModel userModel, string token);
        public Task<string?> SignInAsync(SignInDTO signInDTO);
        public Task<IdentityResult> UpdateAsync(UserModel userModel);
        public Task<IdentityResult> DeleteAsync(UserModel userModel);
    }
}
