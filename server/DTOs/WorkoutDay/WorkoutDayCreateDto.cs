using server.DTOs.WorkoutExercise;

namespace server.DTOs.WorkoutDay
{
    public class WorkoutDayCreateDto
    {
        public DateTime DayOfTheWeek { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<WorkoutExerciseCreateDto> WorkoutExercises { get; set; } = new();

    }
}