using System.ComponentModel.DataAnnotations;
using APIs.Data.User.Models;

namespace APIs.Data.User.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDTO
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, StringLength(30), RegularExpression("[a-zA-Z]{3,30}")]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30), RegularExpression("[a-zA-Z]{3,30}")]
        public string LastName { get; set; } = null!;
        public string University { get => "Assiut University"; }

        /// <summary>
        /// 
        /// </summary>
        public UserDTO() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        public UserDTO(UserModel userModel)
        {
            Id = userModel.Id;
            Email = userModel.Email;
            FirstName = userModel.FirstName;
            LastName = userModel.LastName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        public UserDTO(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Email = userDTO.Email;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
        }
    }
}
