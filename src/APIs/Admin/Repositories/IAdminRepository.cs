using Microsoft.AspNetCore.Identity;

namespace APIs.Admin.Repositories
{
    public interface IAdminRepository
    {
        public Task<IdentityResult> CreateAdminRoleAsync();

        public Task<IdentityResult> AddToRoleAdminAsync(string userId);

        public Task<IdentityResult> CreateUniversityAdminRoleAsync();

        public Task<IdentityResult> AddToRoleUniversityAdminAsync(string userId);

        public Task<IdentityResult> CreateInstructorRoleAsync();
    }
}
