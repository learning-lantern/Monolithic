using API.Auth.DTOs;
using API.Helpers;
using API.User.DTOs;
using API.User.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace API.User.Controllers
{
    /// <summary>
    /// User controller class for user methods.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing the user matching the specified userId if it exists.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> FindById([FromQuery] string userId)
        {
            var user = await userRepository.FindByIdAsync(userId);

            return user == null ? NotFound(JsonConvert.SerializeObject(Message.UserIdNotFound)) :
                Ok(JsonConvert.SerializeObject(user));
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userEmail.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing the user matching the specified userId if it exists.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> FindByEmail([FromQuery] string userEmail)
        {
            var user = await userRepository.FindByEmailAsync(userEmail);

            return user == null ? NotFound(JsonConvert.SerializeObject(Message.UserEmailNotFound)) :
                Ok(JsonConvert.SerializeObject(user));
        }

        /// <summary>
        /// Updates the specified user in the backing store.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            if (!Helper.IsUniversityValid(userDTO.University))
            {
                return BadRequest(JsonConvert.SerializeObject(Message.UniversityNotFound));
            }

            if (!Helper.IsNameValid(userDTO.FirstName) || !Helper.IsNameValid(userDTO.LastName))
            {
                return BadRequest(JsonConvert.SerializeObject(Message.NameNotValid));
            }

            userDTO.Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var user = await userRepository.UpdateAsync(userDTO);

            return user == null ? NotFound(JsonConvert.SerializeObject(Message.UserIdNotFound)) :
                Ok(JsonConvert.SerializeObject(user));
        }

        /// <summary>
        /// Deletes the specified user from the backing store.
        /// </summary>
        /// <param name="signInDTO"></param>
        /// /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] SignInDTO signInDTO)
        {
            if (!Helper.IsUniversityValid(signInDTO.University))
            {
                return BadRequest(JsonConvert.SerializeObject(Message.UniversityNotFound));
            }

            var deleteAsyncResult = await userRepository.DeleteAsync(signInDTO.Email, signInDTO.Password);

            if (!deleteAsyncResult.Succeeded)
            {
                if (deleteAsyncResult.Errors?.FirstOrDefault(error => error.Code == StatusCodes.Status404NotFound.ToString()) != null)
                {
                    return NotFound(JsonConvert.SerializeObject(deleteAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(deleteAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject(Message.UserDeleted));
        }
    }
}
