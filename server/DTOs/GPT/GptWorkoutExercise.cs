using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTOs.GPT
{
    public class GptWorkoutExercise
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal TargetWeight { get; set; }
        public string TargetTime { get; set; } = "00:00:00";
        public string ExerciseName { get; set; } = string.Empty;
    }
}