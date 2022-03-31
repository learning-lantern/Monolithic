using Microsoft.AspNetCore.Identity;

namespace APIs.University.Repositories
{
    public interface IUniversityRepository
    {
        public Task<IdentityResult> AddToRoleInstructorAsync(string userId);
    }
}
