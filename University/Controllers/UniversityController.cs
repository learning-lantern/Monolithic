using API.University.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.University.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize(Roles = "Admin, UniversityAdmin")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository universityAdminRepository;

        public UniversityController(IUniversityRepository universityAdminRepository)
        {
            this.universityAdminRepository = universityAdminRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToRoleInstructor([FromQuery] string university, [FromQuery] string userId)
        {
            if (university != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            var addToRoleInstructorAsyncResult = await universityAdminRepository.AddToRoleInstructorAsync(userId);

            if (!addToRoleInstructorAsyncResult.Succeeded)
            {
                if (addToRoleInstructorAsyncResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(addToRoleInstructorAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(addToRoleInstructorAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject("Updated the user role to Instructor role."));
        }
    }
}
