﻿using System.ComponentModel.DataAnnotations;

namespace APIs.Data.DTOs
{
    /// <summary>
    /// Sign In data transfare object class.
    /// </summary>
    public class SignInDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        /// <summary>
        /// Password property that its pattern must consist of a minimum 6 characters, at least one uppercase letter, one lowercase letter, and one number.
        /// </summary>
        [Required, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&])[a-zA-Z0-9@$!%*?&]{6,}$")]
        public string Password { get; set; } = null!;
    }
}