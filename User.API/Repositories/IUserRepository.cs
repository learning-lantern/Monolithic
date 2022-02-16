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
        public Task<string?> CreateAsync(SignUpDTO signUpModel);
        public Task<UserModel?> FindByIdAsync(string id);
        public Task<UserModel?> FindByEmailAsync(string email);
        public Task<IdentityResult> ConfirmEmailAsync(UserModel user, string token);
        public Task<string?> SendConfirmationEmail(UserModel user);
    }
}
