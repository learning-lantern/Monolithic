using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class User : IdentityUser
    {
        [Key]
        public new int Id { get; }
        public new string Email { get; set; } = null!;
        public new bool EmailConfirmed { get; set; } = false;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateRegisterd { get; set; }
        public string ValidationCode { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        [Column(TypeName = "IMAGE")]
        public byte[]? Image { get; set; }
    }
}
