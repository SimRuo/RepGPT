namespace server.DTOs.WorkoutDay
{
    public class WorkoutDayUpdateDto
    {
        public DateTime DayOfTheWeek { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}