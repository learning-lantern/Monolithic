using System.ComponentModel.DataAnnotations;

namespace User.API.Data.DTOs
{
    /// <summary>
    /// Sign In data transfare object class.
    /// </summary>
    public class SignInDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, RegularExpression("^(?=.*[A-Z].*[a-z])(?=.*[0-9]).{8,30}$")]
        public string Password { get; set; } = null!;
    }
}
