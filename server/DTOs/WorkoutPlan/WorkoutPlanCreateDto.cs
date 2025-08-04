using server.DTOs.WorkoutDay;

namespace server.DTOs.WorkoutPlan
{
    // TODO: Sometimes the model sends an empty workoutplan, need to prevent that
    public class WorkoutPlanCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public Guid UserId { get; set; }

        public List<WorkoutDayCreateDto> WorkoutDays { get; set; } = new();
    }
}
