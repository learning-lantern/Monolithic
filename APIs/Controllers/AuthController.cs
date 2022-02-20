using APIs.Data.DTOs;
using APIs.Repositories.AuthRepository;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authRepository"></param>
        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUpDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SignUpDTO signUpDTO)
        {
            var id = await authRepository.CreateAsync(signUpDTO);

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
            var token = await authRepository.SignInAsync(signInDTO);

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
            var confirmEmailAsyncResult = await authRepository.ConfirmEmailAsync(userId, token);

            return confirmEmailAsyncResult.Succeeded ?
                CreatedAtAction(actionName: nameof(SignIn), value: userId)
                : BadRequest();
        }
    }
}
