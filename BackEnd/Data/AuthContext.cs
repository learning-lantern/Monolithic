using BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class AuthContext : IdentityDbContext<User>
    {
        public DbSet<User>? User { get; set; }

        public AuthContext() { }

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

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
