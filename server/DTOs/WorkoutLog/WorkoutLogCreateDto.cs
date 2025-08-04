namespace server.DTOs.WorkoutLog
{
    public class WorkoutLogCreateDto
    {
        public DateTime Date { get; set; }
        public int CompletedSets { get; set; }
        public int CompletedReps { get; set; }
        public decimal ActualWeight { get; set; }
        public TimeSpan ActualTime { get; set; }
        public string Notes { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public int WorkoutExerciseId { get; set; }
    }
}