namespace server.DTOs.WorkoutExercise
{
    public class WorkoutExerciseUpdateDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal TargetWeight { get; set; }
        public TimeSpan TargetTime { get; set; }
    }
}