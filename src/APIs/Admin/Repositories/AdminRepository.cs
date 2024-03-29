﻿using APIs.Helpers;
using APIs.User.Models;
using Microsoft.AspNetCore.Identity;

namespace APIs.Admin.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<UserModel> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminRepository(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAdminRoleAsync()
        {
            return await roleManager.CreateAsync(new IdentityRole(Role.Admin));
        }

        public async Task<IdentityResult> AddToRoleAdminAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = StatusCodes.Status404NotFound.ToString(),
                    Description = Message.UserIdNotFound
                });
            }

            return await userManager.AddToRoleAsync(user, Role.Admin);
        }

        public async Task<IdentityResult> CreateUniversityAdminRoleAsync()
        {
            return await roleManager.CreateAsync(new IdentityRole(Role.UniversityAdmin));
        }

        public async Task<IdentityResult> AddToRoleUniversityAdminAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = StatusCodes.Status404NotFound.ToString(),
                    Description = Message.UserIdNotFound
                });
            }

            return await userManager.AddToRoleAsync(user, Role.UniversityAdmin);
        }

        public async Task<IdentityResult> CreateInstructorRoleAsync()
        {
            return await roleManager.CreateAsync(new IdentityRole(Role.Instructor));
        }
    }
}
