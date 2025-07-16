using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.GPT;
using server.DTOs.WorkoutPlan;
using server.Models;
using server.Services.Ai;

namespace server.Services
{
    public class WorkoutPlanService
    {
        private readonly RepGPTContext _context;
        private readonly GptService _gptService;
        public WorkoutPlanService(RepGPTContext context, GptService gptService)
        {
            _context = context;
            _gptService = gptService;

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
            // 1. Generate prompt and call GPT
            string prompt = $"Create a workout plan for the following goal: {dto.Goal}.";
            string gptJson = await _gptService.SendPromptAsync(prompt);

            var gptPlan = JsonSerializer.Deserialize<GptWorkoutPlan>(gptJson);
            if (gptPlan == null)
                throw new Exception("Invalid GPT response");

            // 2. Create WorkoutPlan
            var workoutPlan = new WorkoutPlan
            {
                Name = gptPlan.Name,
                Goal = gptPlan.Goal,
                UserId = dto.UserId,
                WorkoutDays = new List<WorkoutDay>()
            };

            foreach (var gptDay in gptPlan.WorkoutDays)
            {
                var workoutDay = new WorkoutDay
                {
                    DayOfTheWeek = DateTime.Parse(gptDay.DayOfTheWeek),
                    Notes = gptDay.Notes,
                    WorkoutExercises = new List<WorkoutExercise>()
                };

                foreach (var gptEx in gptDay.WorkoutExercises)
                {
                    // Lookup or create Exercise
                    var exercise = await _context.Exercises
                        .FirstOrDefaultAsync(e => e.Name == gptEx.ExerciseName);

                    if (exercise == null)
                    {
                        exercise = new Exercise
                        {
                            Name = gptEx.ExerciseName,
                            Equipment = "Bodyweight", // fallback
                            PrimaryMuscleGroup = "General" // fallback
                        };

                        _context.Exercises.Add(exercise);
                        await _context.SaveChangesAsync(); // save now to get Id
                    }

                    workoutDay.WorkoutExercises.Add(new WorkoutExercise
                    {
                        ExerciseId = exercise.Id,
                        Sets = gptEx.Sets,
                        Reps = gptEx.Reps,
                        TargetWeight = gptEx.TargetWeight,
                        TargetTime = TimeSpan.Parse(gptEx.TargetTime)
                    });
                }

                workoutPlan.WorkoutDays.Add(workoutDay);
            }

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
