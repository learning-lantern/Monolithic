﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.API.Data.Models;

namespace User.API.Data
{
    /// <summary>
    /// User data context class, inherits from "IdentityDbContext" class.
    /// </summary>
    public class UserContext : IdentityDbContext<UserModel>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
