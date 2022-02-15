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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUpModel"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<string?> SignUpAsync(SignUpDTO signUpModel)
        {
            UserModel user = await userManager.FindByEmailAsync(signUpModel.Email);

            if (user is not null)
            {
                return null;
            }

            user = new()
            {
                Email = signUpModel.Email,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Image = signUpModel.Image
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
