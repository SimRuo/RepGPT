using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class RepGPTContext : DbContext
    {
        public RepGPTContext(DbContextOptions<RepGPTContext> options) : base(options) { }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PromptHistory> PromptHistorys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkoutLog>()
                .HasOne(wl => wl.WorkoutExercise)
                .WithMany(we => we.WorkoutLogs)
                .HasForeignKey(wl => wl.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}