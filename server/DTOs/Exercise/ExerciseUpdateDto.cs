namespace server.DTOs.Exercise
{
    public class ExerciseUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
        public string PrimaryMuscleGroup { get; set; } = string.Empty;
        public string SecondaryMuscleGroup { get; set; } = string.Empty;
    }
}