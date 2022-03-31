﻿using APIs.Auth.DTOs;
using APIs.Auth.Repositories;
using APIs.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Auth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDTO createDTO)
        {
            if (!Helper.IsUniversityValid(createDTO.University))
            {
                return BadRequest(JsonConvert.SerializeObject(Message.UniversityNotFound));
            }

            if (!Helper.IsNameValid(createDTO.FirstName) || !Helper.IsNameValid(createDTO.LastName))
            {
                return BadRequest(JsonConvert.SerializeObject(Message.NameNotValid));
            }

            var createAsyncResult = await authRepository.CreateAsync(createDTO);

            return createAsyncResult.Succeeded ?
                CreatedAtAction(actionName: nameof(ConfirmEmail),
                value: JsonConvert.SerializeObject(createDTO.Email)) :
                BadRequest(JsonConvert.SerializeObject(createAsyncResult.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            if (!Helper.IsUniversityValid(signInDTO.University))
            {
                return BadRequest(JsonConvert.SerializeObject(Message.UniversityNotFound));
            }

            var signInResponseDTO = await authRepository.SignInAsync(signInDTO);

            if (signInResponseDTO.User == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.UserEmailNotFound));
            }

            return string.IsNullOrEmpty(signInResponseDTO.Token) ?
                Unauthorized(JsonConvert.SerializeObject("Can not get JWT.")) :
                Ok(JsonConvert.SerializeObject(signInResponseDTO));
        }

        [HttpGet]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string userEmail)
        {
            var sendConfirmationEmailResult = await authRepository.SendConfirmationEmailAsync(userEmail);

            if (!sendConfirmationEmailResult.Succeeded)
            {
                if (sendConfirmationEmailResult.Errors?.FirstOrDefault(error => error.Code == StatusCodes.Status404NotFound.ToString()) != null)
                {
                    return NotFound(JsonConvert.SerializeObject(sendConfirmationEmailResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(sendConfirmationEmailResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject(userEmail));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var confirmEmailAsyncResult = await authRepository.ConfirmEmailAsync(userId, token);

            if (!confirmEmailAsyncResult.Succeeded)
            {
                if (confirmEmailAsyncResult.Errors?.FirstOrDefault(error => error.Code == StatusCodes.Status404NotFound.ToString()) != null)
                {
                    return NotFound(JsonConvert.SerializeObject(confirmEmailAsyncResult.Errors));
                }

                return BadRequest(JsonConvert.SerializeObject(confirmEmailAsyncResult.Errors));
            }

            return Ok(JsonConvert.SerializeObject(userId));
        }
    }
}
