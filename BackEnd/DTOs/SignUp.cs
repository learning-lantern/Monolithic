using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs
{
    public class SignUp
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Compare("ConfirmedPassword")]
        [RegularExpression("^(?=.*[A-Z].*[a-z])(?=.*[0-9]).{8,30}$")]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmedPassword { get; set; } = null!;
        [Required, StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(50)]
        public string LastName { get; set; } = null!;
        public byte[]? Image { get; set; }
    }
}
