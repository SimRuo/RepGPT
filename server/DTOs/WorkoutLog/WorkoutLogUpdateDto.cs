namespace server.DTOs.WorkoutLog
{
    public class WorkoutLogUpdateDto
    {
        public int CompletedSets { get; set; }
        public int CompletedReps { get; set; }
        public decimal ActualWeight { get; set; }
        public TimeSpan ActualTime { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}