using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.API.Data;
using User.API.Data.DTOs;
using User.API.Data.Models;

namespace User.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;

        public UserRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpDTO signUpModel)
        {
            using (UserContext db = new())
            {
                string email = signUpModel.Email;

                IQueryable<UserModel>? users = db.Users?.Where(u => u.Email == email);

                if (users is not null)
                {
                    return IdentityResult.Failed(new IdentityError() { Code = "400" });
                }
            }
            return null;
        }

        public async Task<string> CreateUserAsync(SignUpDTO signUpModel)
        {
            var user = new UserModel()
            {
                Email = signUpModel.Email,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Image = signUpModel.Image,
                DateRegisterd = DateTime.Now
            };

            await userManager.CreateAsync(user, signUpModel.Password);
            return user.Id;
        }

        public async Task<UserModel> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
    }
}
