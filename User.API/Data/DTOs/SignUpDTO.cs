using System.ComponentModel.DataAnnotations;

namespace User.API.Data.DTOs
{
    public class SignUpDTO : SignInDTO
    {
        [Required, StringLength(30)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30)]
        public string LastName { get; set; } = null!;
        public byte[]? Image { get; set; }
    }
}
