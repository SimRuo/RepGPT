using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class WorkoutDay
    {
        public int Id { get; set; }
        public string DayOfTheWeek { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public int WorkoutExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; } = null!;
    }
}