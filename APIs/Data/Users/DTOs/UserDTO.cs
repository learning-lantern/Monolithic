using System.ComponentModel.DataAnnotations;
using APIs.Data.Users.Models;

namespace APIs.Data.Users.DTOs
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
        [Required, StringLength(30)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30)]
        public string LastName { get; set; } = null!;
        public byte[]? Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserDTO() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userMode"></param>
        public UserDTO(UserModel userMode)
        {
            Id = userMode.Id;
            Email = userMode.Email;
            FirstName = userMode.FirstName;
            LastName = userMode.LastName;
            Image = userMode.Image;
        }
    }
}
