using APIs.Data.User.DTOs;
using APIs.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    /// <summary>
    /// User controller class for user methods.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// User controller class constructor.
        /// </summary>
        /// <param name="userRepository"></param>
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
        [HttpGet, Authorize]
        public async Task<IActionResult> FindById([FromQuery] string userId)
        {
            var user = await userRepository.FindByIdAsync(userId);

            return user is null ? NotFound(JsonConvert.SerializeObject(
                "There is no user in this University whith this Id."))
                : Ok(JsonConvert.SerializeObject(user));
        }

        /// <summary>
        /// Updates the specified user in the backing store.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpPut("Update"), Authorize]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            if (userDTO.University != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            if (userDTO.FirstName.Replace(" ", "").Length < 2 ||
                userDTO.LastName.Replace(" ", "").Length < 2)
            {
                return BadRequest(JsonConvert.SerializeObject("The first name and last name if they have space, then their alphabetic characters length must be greater than or equal 2."));
            }

            var updateAsyncResult = await userRepository.UpdateAsync(userDTO);

            if (!updateAsyncResult.Succeeded)
            {
                if (updateAsyncResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(updateAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(updateAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject(userDTO));
        }

        /// <summary>
        /// Deletes the specified user from the backing store.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpDelete("Delete"), Authorize]
        public async Task<IActionResult> Delete([FromQuery] string userId)
        {
            var deleteAsyncResult = await userRepository.DeleteAsync(userId);

            if (!deleteAsyncResult.Succeeded)
            {
                if (deleteAsyncResult.Errors?.FirstOrDefault(error => error.Code == "NotFound") is not null)
                {
                    return NotFound(JsonConvert.SerializeObject(deleteAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(deleteAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject("User has been deleted."));
        }
    }
}
