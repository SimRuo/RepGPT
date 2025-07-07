namespace server.DTOs.Exercise
{
    public class ExerciseReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
        public string PrimaryMuscleGroup { get; set; } = string.Empty;
        public string SecondaryMuscleGroup { get; set; } = string.Empty;
    }
}