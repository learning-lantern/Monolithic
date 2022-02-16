using Microsoft.AspNetCore.Mvc;
using User.API.Data.DTOs;
using User.API.Repositories;

namespace User.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO user)
        {
            var id = await userRepository.CreateAsync(user);

            if (id is null)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost("ConfirmEmail/{id}/{token}")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] string id, [FromRoute] string token)
        {
            var user = await userRepository.FindByIdAsync(id);

            if (user is null)
            {
                return BadRequest();
            }

            var confirmEmailAsyncResult = await userRepository.ConfirmEmailAsync(user, token);

            if (confirmEmailAsyncResult.Succeeded)
            {
                return Ok(id);
            }

            return BadRequest();
        }
    }
}
