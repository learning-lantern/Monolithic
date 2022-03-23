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
        public async Task<IActionResult> AddToRoleInstructor([FromQuery] string university, [FromBody] string userName)
        {
            if (university != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            var result = await universityAdminRepository.AddToRoleInstructorAsync(userName);

            if (!result.Succeeded)
            {
                if (result.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(result.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(result.Errors));
            }

            return Ok();
        }
    }
}
