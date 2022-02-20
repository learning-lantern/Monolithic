using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APIs.Data.Models;

namespace APIs.Data
{
    /// <summary>
    /// User data context class, inherits from "IdentityDbContext" class.
    /// </summary>
    public class UserContext : IdentityDbContext<UserModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
