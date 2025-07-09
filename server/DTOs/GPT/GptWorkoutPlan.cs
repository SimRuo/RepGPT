using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTOs.GPT
{
    public class GptWorkoutPlan
    {
        public string Name { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public List<GptWorkoutDay> WorkoutDays { get; set; } = new();
    }
}