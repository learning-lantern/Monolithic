using API.User.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Admin.Repositories
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
            var role = new IdentityRole("Admin");

            return await roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> AddToRoleAdminAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NotFound",
                    Description = "There is no user in this University with this Id."
                });
            }

            return await userManager.AddToRoleAsync(user, "Admin");
        }

        public async Task<IdentityResult> CreateUniversityAdminRoleAsync()
        {
            var role = new IdentityRole("UniversityAdmin");

            return await roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> AddToRoleUniversityAdminAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NotFound",
                    Description = "There is no user in this University with this Id."
                });
            }

            return await userManager.AddToRoleAsync(user, "UniversityAdmin");
        }

        public async Task<IdentityResult> CreateInstructorRoleAsync()
        {
            var role = new IdentityRole("Instructor");

            return await roleManager.CreateAsync(role);
        }
    }
}
