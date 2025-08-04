using server.DTOs.WorkoutDay;

namespace server.DTOs.WorkoutPlan
{
    public class WorkoutPlanReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public Guid UserId { get; set; }

        public List<WorkoutDayReadDto> WorkoutDays { get; set; } = new();
    }
}
