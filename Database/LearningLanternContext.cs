using API.Calendar.Models;
using API.Classroom.Models;
using API.ToDo.Models;
using API.User.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    /// <summary>
    /// User data context class for Entity Framework Core.
    /// </summary>
    public class LearningLanternContext : IdentityDbContext<UserModel>
    {
        public DbSet<TaskModel> Tasks { get; set; } = null!;
        public DbSet<EventModel> Events { get; set; } = null!;
        public DbSet<ClassroomModel> Classrooms { get; set; } = null!;

        public LearningLanternContext(DbContextOptions<LearningLanternContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskModel>()
                .HasOne(task => task.User)
                .WithMany(user => user.Tasks)
                .HasForeignKey(task => task.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<EventModel>()
                .HasOne(Event => Event.Classroom)
                .WithMany(classroom => classroom.Events)
                .HasForeignKey(Event => Event.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
