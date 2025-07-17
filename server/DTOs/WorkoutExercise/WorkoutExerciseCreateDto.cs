namespace server.DTOs.WorkoutExercise
{
    public class WorkoutExerciseCreateDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal TargetWeight { get; set; }
        public TimeSpan TargetTime { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
    }
}
