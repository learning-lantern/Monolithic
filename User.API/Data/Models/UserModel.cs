using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.API.Data.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateRegisterd { get; set; }
        public string ValidationCode { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        [Column(TypeName = "IMAGE")]
        public byte[]? Image { get; set; }
    }
}
