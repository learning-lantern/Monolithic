﻿using API.Auth.DTOs;
using API.Helpers;
using API.User.DTOs;
using API.User.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            return user is null ? NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."))
                : Ok(JsonConvert.SerializeObject(user));
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

            return user is null ? NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Email."))
                : Ok(JsonConvert.SerializeObject(user));
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
        /// <param name="signInUserDTO"></param>
        /// /// <returns>
        /// Task that represents the asynchronous operation, containing IActionResult of the operation.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] SignInUserDTO signInUserDTO)
        {
            if (signInUserDTO.University != "Assiut University")
            {
                return BadRequest(JsonConvert.SerializeObject("There is no University in our database with this name."));
            }

            var deleteAsyncResult = await userRepository.DeleteAsync(signInUserDTO.Email, signInUserDTO.Password);

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
