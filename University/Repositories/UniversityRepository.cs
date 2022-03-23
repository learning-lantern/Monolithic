using API.User.Models;
using Microsoft.AspNetCore.Identity;

namespace API.University.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly UserManager<UserModel> userManager;

        public UniversityRepository(UserManager<UserModel> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleInstructorAsync(string userId)
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

            return await userManager.AddToRoleAsync(user, "Instructor");
        }
    }
}
