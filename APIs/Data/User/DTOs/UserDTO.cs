using System.ComponentModel.DataAnnotations;
using APIs.Data.User.Models;

namespace APIs.Data.User.DTOs
{
    /// <summary>
    /// User data transfare object class.
    /// </summary>
    public class UserDTO
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, StringLength(30), RegularExpression("^((?![0-9!\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]).){2,30}$")]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30), RegularExpression("^((?![0-9!\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]).){2,30}$")]
        public string LastName { get; set; } = null!;
        public string University { get; set; } = null!;

        public UserDTO() { }
        public UserDTO(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Email = userDTO.Email;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            University = userDTO.University;
        }
        public UserDTO(UserModel userModel)
        {
            Id = userModel.Id;
            Email = userModel.Email;
            FirstName = userModel.FirstName;
            LastName = userModel.LastName;
            University = "Assiut University";
        }
    }
}
