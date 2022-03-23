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

        public async Task<IdentityResult> AddToRoleInstructorAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NotFound",
                    Description = "There is no user in this University with this user name."
                });
            }

            return await userManager.AddToRoleAsync(user, "Instructor");
        }
    }
}
