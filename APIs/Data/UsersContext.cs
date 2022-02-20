using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APIs.Data.Models;

namespace APIs.Data
{
    /// <summary>
    /// User data context class, inherits from "IdentityDbContext" class.
    /// </summary>
    public class UsersContext : IdentityDbContext<UserModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
    }
}
