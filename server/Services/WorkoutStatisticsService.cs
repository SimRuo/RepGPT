using System.Globalization;
using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Services
{
    public class WorkoutStatisticsService
    {
        private readonly RepGPTContext _context;

        public WorkoutStatisticsService(RepGPTContext context)
        {
            _context = context;
        }

        private static string GetISOWeek(DateTime date)
        {
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }
            var weekNum = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday
            );
            return $"{date.Year}-W{weekNum}";
        }

        public async Task<object> GetOverallStatsAsync(Guid userId)
        {
            var logs = await _context.WorkoutLogs
                .Where(l => l.UserId == userId)
                .Include(l => l.WorkoutExercise)
                    .ThenInclude(we => we.Exercise)
                .ToListAsync();

            var weeklyVolume = logs
                .GroupBy(l => GetISOWeek(l.Date))
                .Select(g => new
                {
                    Week = g.Key,
                    AvgDailyVolume = g.Sum(x => x.CompletedSets * x.CompletedReps * x.ActualWeight) / 7
                })
                .OrderBy(x => x.Week)
                .ToList();

            var topExercises = logs
                .GroupBy(l => l.WorkoutExercise.Exercise.Name)
                .Select(g => new { Exercise = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            var allExercises = logs
                .Select(l => l.WorkoutExercise.Exercise.Name)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            var workoutsLogged = logs
                .Select(l => l.Date.Date)
                .Distinct()
                .Count();

            var recentWeek = weeklyVolume.LastOrDefault();
            var avgDailyVolumeThisWeek = recentWeek?.AvgDailyVolume ?? 0;

            return new
            {
                AvgDailyVolumeThisWeek = avgDailyVolumeThisWeek,
                WeeklyVolume = weeklyVolume,
                TopExercises = topExercises,
                WorkoutsLogged = workoutsLogged,
                AllExercises = allExercises
            };
        }


        public async Task<object> GetExerciseStatsAsync(Guid userId, string exerciseName)
        {
            var logs = await _context.WorkoutLogs
                .Where(l => l.UserId == userId && l.WorkoutExercise.Exercise.Name == exerciseName)
                .Include(l => l.WorkoutExercise)
                    .ThenInclude(we => we.Exercise)
                .ToListAsync();

            var weeklyVolume = logs
                .GroupBy(l => GetISOWeek(l.Date))
                .Select(g => new
                {
                    Week = g.Key,
                    AvgDailyVolume = g.Sum(x => x.CompletedSets * x.CompletedReps * x.ActualWeight) / 7
                })
                .OrderBy(x => x.Week)
                .ToList();

            var avgWeightPerRep = logs
                .GroupBy(l => GetISOWeek(l.Date))
                .Select(g => new
                {
                    Week = g.Key,
                    AvgWeight = g.Sum(x => x.CompletedReps * x.ActualWeight) /
                                (g.Sum(x => x.CompletedReps) == 0 ? 1 : g.Sum(x => x.CompletedReps))
                })
                .OrderBy(x => x.Week)
                .ToList();

            return new
            {
                WeeklyVolume = weeklyVolume,
                AvgWeightPerRep = avgWeightPerRep
            };
        }
    }
}
