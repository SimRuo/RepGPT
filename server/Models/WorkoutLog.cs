using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class WorkoutLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CompletedSets { get; set; }
        public int CompletedReps { get; set; }
        public decimal ActualWeight { get; set; }
        public TimeSpan ActualTime { get; set; }
        public string Notes { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;


        public int WorkoutExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; } = null!;
    }
}