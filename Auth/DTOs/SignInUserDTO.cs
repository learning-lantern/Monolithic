﻿using System.ComponentModel.DataAnnotations;

namespace API.Auth.DTOs
{
    /// <summary>
    /// Sign In data transfare object class.
    /// </summary>
    public class SignInUserDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        /// <summary>
        /// Password property that its pattern must consist of a minimum 6 characters, at least one uppercase letter, one lowercase letter, one special character, and one number.
        /// </summary>
        [Required, RegularExpression(Patterns.Password)]
        public string Password { get; set; } = null!;
        [Required]
        public string University { get; set; } = null!;
    }
}
