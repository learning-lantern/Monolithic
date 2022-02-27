﻿using APIs.Data.User.DTOs;

namespace APIs.Data.Auth.DTOs
{
    /// <summary>
    /// Sign in response data transfare object class.
    /// </summary>
    public class SignInResponseDTO
    {
        public UserDTO? User;
        public string? Token { get; set; }

        public SignInResponseDTO() { }
        public SignInResponseDTO(UserDTO userDTO, string token)
        {
            User = new UserDTO(userDTO);
            Token = token;
        }
    }
}
