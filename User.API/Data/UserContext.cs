using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.API.Data.Models;

namespace User.API.Data
{
    /// <summary>
    /// User data context class, inherits from "IdentityDbContext" class.
    /// </summary>
    public class UserContext : IdentityDbContext<UserModel>
    {
        public UserContext() { }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        /// <summary>
        /// User context class configuation method for EF Core (Database configurations).
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
