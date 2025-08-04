using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.WorkoutLog;
using server.Models;

namespace server.Services
{
    public class WorkoutLogService
    {
        private readonly RepGPTContext _context;

        public WorkoutLogService(RepGPTContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutLogReadDto>> GetAllAsync()
        {
            return await _context.WorkoutLogs
                .Select(wl => new WorkoutLogReadDto
                {
                    Id = wl.Id,
                    Date = wl.Date,
                    CompletedSets = wl.CompletedSets,
                    CompletedReps = wl.CompletedReps,
                    ActualWeight = wl.ActualWeight,
                    ActualTime = wl.ActualTime,
                    Notes = wl.Notes,
                    UserId = wl.User.Id,
                    WorkoutExerciseId = wl.WorkoutExercise.Id,
                    ExerciseName = wl.WorkoutExercise.Exercise.Name
                })
                .ToListAsync();
        }

        public async Task<WorkoutLogReadDto?> GetByIdAsync(int id)
        {
            return await _context.WorkoutLogs
                .Where(wl => wl.Id == id)
                .Select(wl => new WorkoutLogReadDto
                {
                    Id = wl.Id,
                    Date = wl.Date,
                    CompletedSets = wl.CompletedSets,
                    CompletedReps = wl.CompletedReps,
                    ActualWeight = wl.ActualWeight,
                    ActualTime = wl.ActualTime,
                    Notes = wl.Notes,
                    UserId = wl.User.Id,
                    WorkoutExerciseId = wl.WorkoutExercise.Id,
                    ExerciseName = wl.WorkoutExercise.Exercise.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutLogReadDto> CreateAsync(WorkoutLogCreateDto dto)
        {
            var workoutLog = new WorkoutLog
            {
                Date = dto.Date,
                CompletedSets = dto.CompletedSets,
                CompletedReps = dto.CompletedReps,
                ActualWeight = dto.ActualWeight,
                ActualTime = dto.ActualTime,
                Notes = dto.Notes,
                UserId = dto.UserId,
                WorkoutExerciseId = dto.WorkoutExerciseId
            };

            _context.WorkoutLogs.Add(workoutLog);
            await _context.SaveChangesAsync();

            return new WorkoutLogReadDto
            {
                Id = workoutLog.Id,
                Date = workoutLog.Date,
                CompletedSets = workoutLog.CompletedSets,
                CompletedReps = workoutLog.CompletedReps,
                ActualWeight = workoutLog.ActualWeight,
                ActualTime = workoutLog.ActualTime,
                Notes = workoutLog.Notes,
                UserId = workoutLog.UserId,
                WorkoutExerciseId = workoutLog.WorkoutExerciseId
            };
        }

        public async Task<bool> UpdateAsync(int id, WorkoutLogUpdateDto dto)
        {
            var workoutLog = await _context.WorkoutLogs.FindAsync(id);
            if (workoutLog == null) return false;

            workoutLog.CompletedSets = dto.CompletedSets;
            workoutLog.CompletedReps = dto.CompletedReps;
            workoutLog.ActualWeight = dto.ActualWeight;
            workoutLog.ActualTime = dto.ActualTime;
            workoutLog.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var workoutLog = await _context.WorkoutLogs.FindAsync(id);
            if (workoutLog == null) return false;

            _context.WorkoutLogs.Remove(workoutLog);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<WorkoutLogReadDto>> GetByUserIdAsync(Guid userId)
        {
            return await _context.WorkoutLogs
                .Where(wl => wl.UserId == userId)
                .Select(wl => new WorkoutLogReadDto
                {
                    Id = wl.Id,
                    Date = wl.Date,
                    CompletedSets = wl.CompletedSets,
                    CompletedReps = wl.CompletedReps,
                    ActualWeight = wl.ActualWeight,
                    ActualTime = wl.ActualTime,
                    Notes = wl.Notes,
                    UserId = wl.UserId,
                    WorkoutExerciseId = wl.WorkoutExercise.Id,
                    ExerciseName = wl.WorkoutExercise.Exercise.Name
                })
                .ToListAsync();
        }

    }
}
