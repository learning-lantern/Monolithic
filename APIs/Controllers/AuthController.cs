using APIs.Data.Auth.DTOs;
using APIs.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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
            var createAsyncResult = await authRepository.CreateAsync(signUpDTO);

            return createAsyncResult.Succeeded ? Ok() : BadRequest();
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
