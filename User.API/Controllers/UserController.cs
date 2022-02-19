using Microsoft.AspNetCore.Authorization;
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
        /// <param name="signUpDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            var id = await userRepository.CreateAsync(signUpDTO);

            return string.IsNullOrEmpty(id) ? BadRequest() : Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            var token = await userRepository.SignInAsync(signInDTO);

            return string.IsNullOrEmpty(token) ? Unauthorized() : Ok(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var confirmEmailAsyncResult = await userRepository.ConfirmEmailAsync(userId, token);

            return confirmEmailAsyncResult.Succeeded ?
                CreatedAtAction(actionName: nameof(SignIn), value: userId)
                : BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> GetUser([FromQuery] string userId)
        {
            var user = await userRepository.FindByIdAsync(userId);

            return user is null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPut("Update"), Authorize]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            var updateAsyncResult = await userRepository.UpdateAsync(userDTO);

            return updateAsyncResult.Succeeded ?
                CreatedAtAction(actionName: nameof(GetUser), value: userDTO)
                : BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpDelete("Delete"), Authorize]
        public async Task<IActionResult> Delete([FromQuery] string userId)
        {
            var deleteAsyncResult = await userRepository.DeleteAsync(userId);

            return deleteAsyncResult.Succeeded ? Ok() : BadRequest();
        }
    }
}
