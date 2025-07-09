using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTOs.GPT
{
    public class GptWorkoutDay
    {
        public string DayOfTheWeek { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public List<GptWorkoutExercise> WorkoutExercises { get; set; } = new();
    }
}