using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.API.Data.Models
{
    /// <summary>
    /// User data model class, inherits from "IdentityUser" class.
    /// </summary>
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateRegisterd { get; set; } = DateTime.Now;
        public string ValidationCode { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        [Column(TypeName = "IMAGE")]
        public byte[]? Image { get; set; }
    }
}
