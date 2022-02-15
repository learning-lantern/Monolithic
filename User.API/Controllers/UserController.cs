using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.API.Data.DTOs;
using User.API.Repositories;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO user)
        {
            var id = await userRepository.SignUpAsync(user);

            if (id is null)
            {
                return BadRequest();
            }

            return Ok(id);
        }
    }
}
