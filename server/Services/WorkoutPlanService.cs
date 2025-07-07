using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.WorkoutPlan;
using server.Models;

namespace server.Services
{
    public class WorkoutPlanService
    {
        private readonly RepGPTContext _context;

        public WorkoutPlanService(RepGPTContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutPlanReadDto>> GetAllAsync()
        {
            return await _context.WorkoutPlans
                .Select(wp => new WorkoutPlanReadDto
                {
                    Id = wp.Id,
                    Name = wp.Name,
                    Goal = wp.Goal,
                    UserId = wp.UserId
                })
                .ToListAsync();
        }

        public async Task<WorkoutPlanReadDto?> GetByIdAsync(int id)
        {
            return await _context.WorkoutPlans
                .Where(wp => wp.Id == id)
                .Select(wp => new WorkoutPlanReadDto
                {
                    Id = wp.Id,
                    Name = wp.Name,
                    Goal = wp.Goal,
                    UserId = wp.UserId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutPlanReadDto> CreateAsync(WorkoutPlanCreateDto dto)
        {
            var workoutPlan = new WorkoutPlan
            {
                Name = dto.Name,
                Goal = dto.Goal,
                UserId = dto.UserId
            };

            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();

            return new WorkoutPlanReadDto
            {
                Id = workoutPlan.Id,
                Name = workoutPlan.Name,
                Goal = workoutPlan.Goal,
                UserId = workoutPlan.UserId
            };
        }

        public async Task<bool> UpdateAsync(int id, WorkoutPlanUpdateDto dto)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan == null) return false;

            plan.Name = dto.Name;
            plan.Goal = dto.Goal;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan == null) return false;

            _context.WorkoutPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


/* WorkoutExerciseService

WorkoutLogService

PromptHistoryService */