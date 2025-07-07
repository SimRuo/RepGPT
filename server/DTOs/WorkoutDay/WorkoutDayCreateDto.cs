namespace server.DTOs.WorkoutDay
{
    public class WorkoutDayCreateDto
    {
        public DateTime DayOfTheWeek { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int WorkoutPlanId { get; set; }
    }
}