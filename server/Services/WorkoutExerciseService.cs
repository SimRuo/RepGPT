using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.WorkoutExercise;
using server.Models;

namespace server.Services
{
    public class WorkoutExerciseService
    {
        private readonly RepGPTContext _context;

        public WorkoutExerciseService(RepGPTContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutExerciseReadDto>> GetAllAsync()
        {
            return await _context.WorkoutExercises
                .Select(e => new WorkoutExerciseReadDto
                {
                    Id = e.Id,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    TargetWeight = e.TargetWeight,
                    TargetTime = e.TargetTime
                })
                .ToListAsync();
        }

        public async Task<WorkoutExerciseReadDto?> GetByIdAsync(int id)
        {
            return await _context.WorkoutExercises
                .Where(e => e.Id == id)
                .Select(e => new WorkoutExerciseReadDto
                {
                    Id = e.Id,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    TargetWeight = e.TargetWeight,
                    TargetTime = e.TargetTime
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutExerciseReadDto> CreateAsync(WorkoutExerciseCreateDto dto)
        {
            var exercise = new WorkoutExercise
            {
                Sets = dto.Sets,
                Reps = dto.Reps,
                TargetWeight = dto.TargetWeight,
                TargetTime = dto.TargetTime,
                WorkoutDayId = dto.WorkoutDayId,
                ExerciseId = dto.ExerciseId
            };

            _context.WorkoutExercises.Add(exercise);
            await _context.SaveChangesAsync();

            return new WorkoutExerciseReadDto
            {
                Id = exercise.Id,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                TargetWeight = exercise.TargetWeight,
                TargetTime = exercise.TargetTime
            };
        }

        public async Task<bool> UpdateAsync(int id, WorkoutExerciseUpdateDto dto)
        {
            var exercise = await _context.WorkoutExercises.FindAsync(id);
            if (exercise == null) return false;

            exercise.Sets = dto.Sets;
            exercise.Reps = dto.Reps;
            exercise.TargetWeight = dto.TargetWeight;
            exercise.TargetTime = dto.TargetTime;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exercise = await _context.WorkoutExercises.FindAsync(id);
            if (exercise == null) return false;

            _context.WorkoutExercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
