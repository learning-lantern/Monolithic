using API.Data.DTOs;
using API.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    /// <summary>
    /// Auth controller class for authentication methods.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        /// <summary>
        /// Creates the specified user in the backing store with given password, as an asynchronous operation.
        /// </summary>
        /// <param name="createUserDTO"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO)
        {
            if (createUserDTO.University != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            if (createUserDTO.FirstName.Replace(" ", "").Length < 2 ||
                createUserDTO.LastName.Replace(" ", "").Length < 2)
            {
                return BadRequest(JsonConvert.SerializeObject("The first name and last name if they have spaces, then their alphabetic characters length must be greater than or equal 2."));
            }

            var createAsyncResult = await authRepository.CreateAsync(createUserDTO);

            return createAsyncResult.Succeeded ?
                CreatedAtAction(actionName: nameof(ConfirmEmail),
                value: JsonConvert.SerializeObject(createUserDTO.Email))
                : BadRequest(JsonConvert.SerializeObject(createAsyncResult.Errors));
        }

        /// <summary>
        /// Attempts to sign in the specified userName and password combination as an asynchronous operation.
        /// </summary>
        /// <param name="signInUserDTO"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation. The signed in user and its JWT token (object of sign in response data transfare class).
        /// </returns>
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserDTO signInUserDTO)
        {
            if (signInUserDTO.University != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            var signInUserResponseDTO = await authRepository.SignInAsync(signInUserDTO);

            if (signInUserResponseDTO.User is null)
            {
                return NotFound(JsonConvert.SerializeObject("There is no user in this University whith this email."));
            }

            return string.IsNullOrEmpty(signInUserResponseDTO.Token)
                ? Unauthorized(JsonConvert.SerializeObject(
                    "Unauthorized user (Can't get JWT token)."))
                : Ok(JsonConvert.SerializeObject(signInUserResponseDTO));
        }

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var confirmEmailAsyncResult = await authRepository.ConfirmEmailAsync(userId, token);

            if (!confirmEmailAsyncResult.Succeeded)
            {
                if (confirmEmailAsyncResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(confirmEmailAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(confirmEmailAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject(userId));
        }

        /// <summary>
        /// Resends confirmation email token to the email.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpGet("ResendConfirmationEmail")]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string userEmail)
        {
            var sendConfirmationEmailResult = await authRepository.SendConfirmationEmailAsync(userEmail);

            if (!sendConfirmationEmailResult.Succeeded)
            {
                if (sendConfirmationEmailResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(sendConfirmationEmailResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(sendConfirmationEmailResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject(userEmail));
        }
    }
}
