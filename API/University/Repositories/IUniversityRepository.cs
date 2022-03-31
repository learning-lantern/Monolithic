using Microsoft.AspNetCore.Identity;

namespace API.University.Repositories
{
    public interface IUniversityRepository
    {
        public Task<IdentityResult> AddToRoleInstructorAsync(string userId);
    }
}
