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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Database");
            path = Path.Combine(path, "learning-lantern.aun.eg.db");
            options.UseSqlite($"Filename={path}");

            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskModel>()
                .HasOne(taskModel => taskModel.User)
                .WithMany(userModel => userModel.Tasks)
                .HasForeignKey(taskModel => taskModel.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EventModel>()
                .HasOne(eventModel => eventModel.Classroom)
                .WithMany(classroomModel => classroomModel.Events)
                .HasForeignKey(eventModel => eventModel.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
