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

            return result.Succeeded ? Ok() : BadRequest(JsonConvert.SerializeObject(result.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> AddToRoleAdmin([FromBody] string userName)
        {
            var result = await adminRepository.AddToRoleAdminAsync(userName);

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

        [HttpGet]
        public async Task<IActionResult> CreateUniversityAdminRole()
        {
            var result = await adminRepository.CreateUniversityAdminRoleAsync();

            return result.Succeeded ? Ok() : BadRequest(JsonConvert.SerializeObject(result.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> AddToRoleUniversityAdmin([FromQuery] string university, [FromBody] string userName)
        {
            if (university != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            var result = await adminRepository.AddToRoleUniversityAdminAsync(userName);

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

        [HttpGet]
        public async Task<IActionResult> CreateInstructorRole()
        {
            var result = await adminRepository.CreateInstructorRoleAsync();

            return result.Succeeded ? Ok() : BadRequest(JsonConvert.SerializeObject(result.Errors));
        }
    }
}
