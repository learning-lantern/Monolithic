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
        [Required, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,30}$")]
        public string Password { get; set; } = null!;
    }
}
