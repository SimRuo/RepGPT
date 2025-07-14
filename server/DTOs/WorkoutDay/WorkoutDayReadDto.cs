using server.DTOs.WorkoutExercise;

namespace server.DTOs.WorkoutDay
{
    public class WorkoutDayReadDto
    {
        public int Id { get; set; }
        public DateTime DayOfTheWeek { get; set; }
        public string Notes { get; set; } = string.Empty;

        public List<WorkoutExerciseReadDto> WorkoutExercises { get; set; } = new();
    }
}
