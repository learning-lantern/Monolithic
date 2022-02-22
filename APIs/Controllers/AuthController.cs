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
        /// <param name="createDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateDTO createDTO)
        {
            var createAsyncResult = await authRepository.CreateAsync(createDTO);

            return createAsyncResult.Succeeded ?
                CreatedAtAction(actionName: nameof(ConfirmEmail),
                value: "\"" + createDTO.Email + "\"") : BadRequest();
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
            var signInRO = await authRepository.SignInAsync(signInDTO);

            return string.IsNullOrEmpty(signInRO.Token) ? Unauthorized() : Ok(signInRO);
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
                CreatedAtAction(actionName: nameof(SignIn),
                value: "\"" + userId + "\"") : BadRequest();
        }
    }
}
