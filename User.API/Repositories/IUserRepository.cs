using Microsoft.AspNetCore.Identity;
using User.API.Data.DTOs;

namespace User.API.Repositories
{
    public interface IUserRepository
    {
        public Task<string?> SignUpAsync(SignUpDTO signUpModel);
    }
}
