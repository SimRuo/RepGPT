using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.Exercise;
using server.Models;

namespace server.Services
{
    public class ExerciseService
    {
        private readonly RepGPTContext _context;

        public ExerciseService(RepGPTContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseReadDto>> GetAllAsync()
        {
            return await _context.Exercises
                .Select(e => new ExerciseReadDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Equipment = e.Equipment,
                    PrimaryMuscleGroup = e.PrimaryMuscleGroup,
                    SecondaryMuscleGroup = e.SecondaryMuscleGroup
                })
                .ToListAsync();
        }

        public async Task<ExerciseReadDto?> GetByIdAsync(int id)
        {
            return await _context.Exercises
                .Where(e => e.Id == id)
                .Select(e => new ExerciseReadDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Equipment = e.Equipment,
                    PrimaryMuscleGroup = e.PrimaryMuscleGroup,
                    SecondaryMuscleGroup = e.SecondaryMuscleGroup
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ExerciseReadDto> CreateAsync(ExerciseCreateDto dto)
        {
            var exercise = new Exercise
            {
                Name = dto.Name,
                Equipment = dto.Equipment,
                PrimaryMuscleGroup = dto.PrimaryMuscleGroup,
                SecondaryMuscleGroup = dto.SecondaryMuscleGroup
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return new ExerciseReadDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Equipment = exercise.Equipment,
                PrimaryMuscleGroup = exercise.PrimaryMuscleGroup,
                SecondaryMuscleGroup = exercise.SecondaryMuscleGroup
            };
        }

        public async Task<bool> UpdateAsync(int id, ExerciseUpdateDto dto)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return false;

            exercise.Name = dto.Name;
            exercise.Equipment = dto.Equipment;
            exercise.PrimaryMuscleGroup = dto.PrimaryMuscleGroup;
            exercise.SecondaryMuscleGroup = dto.SecondaryMuscleGroup;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return false;

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
