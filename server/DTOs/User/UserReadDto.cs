using server.DTOs.WorkoutPlan;

namespace server.DTOs.User
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public WorkoutPlanReadDto? WorkoutPlan { get; set; }
    }
}
