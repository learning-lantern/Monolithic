using Microsoft.AspNetCore.Mvc;
using User.API.Data.DTOs;
using User.API.Data.Models;
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
        /// <param name="id"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var user = await userRepository.FindByIdAsync(id);

            return user is null ? BadRequest() : Ok(value: user);
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

            return string.IsNullOrEmpty(value: id) ? BadRequest() : Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet("ConfirmEmail/{id}")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] string id, [FromBody] string token)
        {
            var user = await userRepository.FindByIdAsync(id);

            if (user is null)
            {
                return BadRequest();
            }

            if (user.EmailConfirmed)
            {
                return Accepted(value: id);
            }

            var confirmEmailAsyncResult = await userRepository.ConfirmEmailAsync(user, token);

            if (!confirmEmailAsyncResult.Succeeded)
            {
                return BadRequest();
            }

            return CreatedAtAction(actionName: nameof(SignIn), value: user.Id);
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

            return string.IsNullOrEmpty(value: token) ? Unauthorized() : Ok(value: token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserModel userModel)
        {
            var updateAsyncResult = await userRepository.UpdateAsync(userModel);

            return updateAsyncResult.Succeeded ? CreatedAtAction(actionName: nameof(GetUser), controllerName: nameof(UserController), routeValues: userModel.Id, value: userModel.Id) : BadRequest();
        }
    }
}
