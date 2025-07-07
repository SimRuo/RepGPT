using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class WorkoutDay
    {
        public int Id { get; set; }
        public DateTime DayOfTheWeek { get; set; }
        public string Notes { get; set; } = string.Empty;

        public List<WorkoutExercise> WorkoutExercises { get; set; } = new();
        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } = null!;
    }
}