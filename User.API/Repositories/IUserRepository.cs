using User.API.Data.DTOs;
using User.API.Data.Models;

namespace User.API.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
        public Task<string?> SignUpAsync(SignUpDTO signUpModel);
        public Task<UserModel> FindByIdAsync(string id);
        public Task<UserModel> FindByEmailAsync(string email);
    }
}
