using Microsoft.AspNetCore.Identity;

namespace API.Admin.Repositories
{
    public interface IAdminRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> CreateAdminRoleAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> AddToRoleAdminAsync(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> CreateUniversityAdminRoleAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> AddToRoleUniversityAdminAsync(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public Task<IdentityResult> CreateInstructorRoleAsync();
    }
}
