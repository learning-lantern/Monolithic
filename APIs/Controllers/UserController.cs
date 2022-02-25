using APIs.Data.User.DTOs;
using APIs.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> FindById([FromQuery] string userId)
        {
            var user = await userRepository.FindByIdAsync(userId);

            return user is null ? NotFound() : Ok(JsonConvert.SerializeObject(user));
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
                CreatedAtAction(actionName: nameof(FindById),
                value: JsonConvert.SerializeObject(userDTO)) : BadRequest();
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
