﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Data.Models;

namespace API.Data
{
    /// <summary>
    /// User data context class for Entity Framework Core.
    /// </summary>
    public class LearningLanternContext : IdentityDbContext<UserModel>
    {
        public DbSet<TaskModel> Task { get; set; } = null!;

        public LearningLanternContext(DbContextOptions<LearningLanternContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskModel>()
                .HasOne(task => task.User)
                .WithMany(user => user.Tasks)
                .HasForeignKey(task => task.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}