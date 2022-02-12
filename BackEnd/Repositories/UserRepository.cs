using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> SignUpAsync(SignUp signUpModel)
        {
            using (AuthContext db = new())
            {
                string email = signUpModel.Email;

                IQueryable<User>? users = db.User?.Where(u => u.Email == email);

                if (users is not null)
                {
                    return IdentityResult.Failed(new IdentityError() { Code = "400" });
                }
            }

            var user = new User();

            return await userManager.CreateAsync(user, signUpModel.Password);
        }
    }
}
