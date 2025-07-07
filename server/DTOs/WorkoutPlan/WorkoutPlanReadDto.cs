namespace server.DTOs.WorkoutPlan
{
    public class WorkoutPlanReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public int UserId { get; set; }

    }
}