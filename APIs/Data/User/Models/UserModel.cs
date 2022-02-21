using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIs.Data.User.Models
{
    /// <summary>
    /// User data model class, inherits from "IdentityUser" class.
    /// </summary>
    public class UserModel : IdentityUser
    {
        [StringLength(30)]
        public string FirstName { get; set; } = null!;
        [StringLength(30)]
        public string LastName { get; set; } = null!;
        [Column(TypeName = "IMAGE")]
        public byte[]? Image { get; set; }
    }
}
