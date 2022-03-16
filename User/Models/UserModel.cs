﻿using System.ComponentModel.DataAnnotations;
using API.ToDo.Models;
using Microsoft.AspNetCore.Identity;

namespace API.User.Models
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

        public ICollection<TaskModel> Tasks { get; set; } = null!;
    }
}