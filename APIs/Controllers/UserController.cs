using APIs.Data.DTOs;
using APIs.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
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
        /// <param name="userId"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> FindById([FromQuery] string userId)
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
                CreatedAtAction(actionName: nameof(FindById), value: userDTO)
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
