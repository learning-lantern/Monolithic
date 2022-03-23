using API.Admin.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        [HttpGet]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await adminRepository.CreateAdminRoleAsync();

            return result.Succeeded ?
                Ok(JsonConvert.SerializeObject("Created the Admin role.")) :
                BadRequest(JsonConvert.SerializeObject(result.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> AddToRoleAdmin([FromQuery] string userId)
        {
            var addToRoleAdminAsyncResult = await adminRepository.AddToRoleAdminAsync(userId);

            if (!addToRoleAdminAsyncResult.Succeeded)
            {
                if (addToRoleAdminAsyncResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(addToRoleAdminAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(addToRoleAdminAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject("Updated the user role to Admin role."));
        }

        [HttpGet]
        public async Task<IActionResult> CreateUniversityAdminRole()
        {
            var createUniversityAdminRoleAsyncResult = await adminRepository.CreateUniversityAdminRoleAsync();

            return createUniversityAdminRoleAsyncResult.Succeeded ?
                Ok(JsonConvert.SerializeObject("Created the UniversityAdmin role.")) :
                BadRequest(JsonConvert.SerializeObject(createUniversityAdminRoleAsyncResult.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> AddToRoleUniversityAdmin([FromQuery] string university, [FromQuery] string userId)
        {
            if (university != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            var addToRoleUniversityAdminAsyncResult = await adminRepository.AddToRoleUniversityAdminAsync(userId);

            if (!addToRoleUniversityAdminAsyncResult.Succeeded)
            {
                if (addToRoleUniversityAdminAsyncResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(addToRoleUniversityAdminAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(addToRoleUniversityAdminAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject("Updated the user role to UniversityAdmin role."));
        }

        [HttpGet]
        public async Task<IActionResult> CreateInstructorRole()
        {
            var createInstructorRoleAsyncResult = await adminRepository.CreateInstructorRoleAsync();

            return createInstructorRoleAsyncResult.Succeeded ?
                Ok(JsonConvert.SerializeObject("Created the Instructor role.")) :
                BadRequest(JsonConvert.SerializeObject(createInstructorRoleAsyncResult.Errors));
        }
    }
}
