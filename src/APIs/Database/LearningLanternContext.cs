using APIs.Calendar.Models;
using APIs.Classroom.Models;
using APIs.Exam.Models;
using APIs.Quiz.Models;
using APIs.ToDo.Models;
using APIs.User.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIs.Database
{
    /// <summary>
    /// User data context class for Entity Framework Core.
    /// </summary>
    public class LearningLanternContext : IdentityDbContext<UserModel>
    {
        public DbSet<TaskModel> Tasks { get; set; } = null!;
        public DbSet<ClassroomModel> Classrooms { get; set; } = null!;
        public DbSet<ClassroomUserModel> ClassroomUsers { get; set; } = null!;
        public DbSet<EventModel> Events { get; set; } = null!;
        public DbSet<QuizModel> Quizes { get; set; } = null!;
        public DbSet<ExamModel> Exams { get; set; } = null!;

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

            builder.Entity<ClassroomUserModel>()
                .HasKey(classroomUserModel => new { classroomUserModel.ClassroomId, classroomUserModel.UserId });

            builder.Entity<ClassroomUserModel>()
                .HasOne(classroomUserModel => classroomUserModel.User)
                .WithMany(userModel => userModel.ClassroomUsers)
                .HasForeignKey(classroomUserModel => classroomUserModel.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ClassroomUserModel>()
                .HasOne(classroomUserModel => classroomUserModel.Classroom)
                .WithMany(classroomModel => classroomModel.ClassroomUsers)
                .HasForeignKey(classroomUserModel => classroomUserModel.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EventModel>()
                .HasOne(eventModel => eventModel.Classroom)
                .WithMany(classroomModel => classroomModel.Events)
                .HasForeignKey(eventModel => eventModel.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizModel>()
                .HasOne(quizModel => quizModel.User)
                .WithMany(userModel => userModel.Quizes)
                .HasForeignKey(quizModel => quizModel.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuizModel>()
                .HasOne(quizModel => quizModel.Classroom)
                .WithMany(classroomModel => classroomModel.Quizes)
                .HasForeignKey(quizModel => quizModel.ClassroomId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}
