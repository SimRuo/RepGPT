using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal TargetWeight { get; set; }
        public TimeSpan TargetTime { get; set; }


        public int WorkoutDayId { get; set; }
        public WorkoutDay WorkoutDay { get; set; } = null!;

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public List<WorkoutLog> WorkoutLogs { get; set; } = new();
    }
}