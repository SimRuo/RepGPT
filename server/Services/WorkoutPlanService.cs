using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.GPT;
using server.DTOs.WorkoutDay;
using server.DTOs.WorkoutExercise;
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
                .Include(wp => wp.WorkoutDays)
                    .ThenInclude(d => d.WorkoutExercises)
                        .ThenInclude(we => we.Exercise)
                .Select(wp => new WorkoutPlanReadDto
                {
                    Id = wp.Id,
                    Name = wp.Name,
                    Goal = wp.Goal,
                    UserId = wp.UserId,
                    WorkoutDays = wp.WorkoutDays.Select(d => new WorkoutDayReadDto
                    {
                        Id = d.Id,
                        DayOfTheWeek = d.DayOfTheWeek,
                        Notes = d.Notes,
                        WorkoutExercises = d.WorkoutExercises.Select(e => new WorkoutExerciseReadDto
                        {
                            Id = e.Id,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            TargetWeight = e.TargetWeight,
                            TargetTime = e.TargetTime,
                            ExerciseName = e.Exercise.Name
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<WorkoutPlanReadDto?> GetByIdAsync(int id)
        {
            return await _context.WorkoutPlans
                .Include(wp => wp.WorkoutDays)
                    .ThenInclude(d => d.WorkoutExercises)
                        .ThenInclude(e => e.Exercise)
                .Where(wp => wp.Id == id)
                .Select(wp => new WorkoutPlanReadDto
                {
                    Id = wp.Id,
                    Name = wp.Name,
                    Goal = wp.Goal,
                    UserId = wp.UserId,
                    WorkoutDays = wp.WorkoutDays.Select(d => new WorkoutDayReadDto
                    {
                        Id = d.Id,
                        DayOfTheWeek = d.DayOfTheWeek,
                        Notes = d.Notes,
                        WorkoutExercises = d.WorkoutExercises.Select(e => new WorkoutExerciseReadDto
                        {
                            Id = e.Id,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            TargetWeight = e.TargetWeight,
                            TargetTime = e.TargetTime,
                            ExerciseName = e.Exercise.Name
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutPlanReadDto> CreateAsync(WorkoutPlanCreateDto dto)
        {
            var workoutPlan = new WorkoutPlan
            {
                Name = dto.Name,
                Goal = dto.Goal,
                UserId = dto.UserId,
                WorkoutDays = new List<WorkoutDay>()
            };

            foreach (var dayDto in dto.WorkoutDays)
            {
                var workoutDay = new WorkoutDay
                {
                    DayOfTheWeek = dayDto.DayOfTheWeek,
                    Notes = dayDto.Notes,
                    WorkoutExercises = new List<WorkoutExercise>()
                };

                foreach (var exDto in dayDto.WorkoutExercises)
                {
                    // Find or create exercise
                    var exercise = await _context.Exercises
                        .FirstOrDefaultAsync(e => e.Name.ToLower() == exDto.ExerciseName.ToLower());

                    if (exercise == null)
                    {
                        exercise = new Exercise
                        {
                            Name = exDto.ExerciseName,
                            Equipment = "Bodyweight", // Default
                            PrimaryMuscleGroup = "General" // Default
                        };

                        _context.Exercises.Add(exercise);
                        await _context.SaveChangesAsync(); // Ensure exercise.Id is available
                    }

                    workoutDay.WorkoutExercises.Add(new WorkoutExercise
                    {
                        Sets = exDto.Sets,
                        Reps = exDto.Reps,
                        TargetWeight = exDto.TargetWeight,
                        TargetTime = exDto.TargetTime,
                        ExerciseId = exercise.Id
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
        public async Task<List<WorkoutPlanReadDto>> GetByUserIdAsync(int userId)
        {
            return await _context.WorkoutPlans
                .Where(wp => wp.UserId == userId)
                .Include(wp => wp.WorkoutDays)
                    .ThenInclude(wd => wd.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .Select(wp => new WorkoutPlanReadDto
                {
                    Id = wp.Id,
                    Name = wp.Name,
                    Goal = wp.Goal,
                    UserId = wp.UserId,
                    WorkoutDays = wp.WorkoutDays.Select(d => new WorkoutDayReadDto
                    {
                        Id = d.Id,
                        DayOfTheWeek = d.DayOfTheWeek,
                        Notes = d.Notes,
                        WorkoutExercises = d.WorkoutExercises.Select(e => new WorkoutExerciseReadDto
                        {
                            Id = e.Id,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            TargetWeight = e.TargetWeight,
                            TargetTime = e.TargetTime,
                            ExerciseName = e.Exercise.Name
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
        }

    }
}
