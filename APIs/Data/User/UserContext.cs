using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APIs.Data.User.Models;

namespace APIs.Data.User
{
    /// <summary>
    /// User data context class for Entity Framework Core.
    /// </summary>
    public class UserContext : IdentityDbContext<UserModel>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
