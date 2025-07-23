namespace server.DTOs.WorkoutExercise
{
    // TODO: Sometimes the model will send "8-10 reps" resulting in a 400 bad request. Fix
    public class WorkoutExerciseCreateDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal TargetWeight { get; set; }
        public TimeSpan TargetTime { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
    }
}
