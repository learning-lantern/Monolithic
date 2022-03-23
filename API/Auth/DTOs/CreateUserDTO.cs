using System.ComponentModel.DataAnnotations;

namespace API.Auth.DTOs
{
    /// <summary>
    /// Create data transfare object class, inherits from "SignInDTO" class.
    /// </summary>
    public class CreateUserDTO : SignInUserDTO
    {
        [Required, StringLength(30), RegularExpression(Patterns.Name)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30), RegularExpression(Patterns.Name)]
        public string LastName { get; set; } = null!;
    }
}
