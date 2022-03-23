using Microsoft.AspNetCore.Identity;

namespace API.University.Repositories
{
    public interface IUniversityRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> AddToRoleInstructorAsync(string userId);
    }
}
