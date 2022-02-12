using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs
{
    public class LogIn
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, RegularExpression("^(?=.*[A-Z].*[a-z])(?=.*[0-9]).{8,30}$")]
        public string Password { get; set; } = null!;
    }
}
