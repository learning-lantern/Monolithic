﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.API.Data.Models
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
        public DateTime DateRegisterd { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; } = false;
        [Column(TypeName = "IMAGE")]
        public byte[]? Image { get; set; }
    }
}
