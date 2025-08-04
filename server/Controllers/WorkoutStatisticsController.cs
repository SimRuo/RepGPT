using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutStatisticsController : ControllerBase
    {
        private readonly WorkoutStatisticsService _service;

        public WorkoutStatisticsController(WorkoutStatisticsService service)
        {
            _service = service;
        }

        // GET: api/workoutstatistics/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOverallStats(Guid userId)
        {
            var stats = await _service.GetOverallStatsAsync(userId);
            return Ok(stats);
        }

        // GET: api/workoutstatistics/exercise/5/Bench%20Press
        [HttpGet("exercise/{userId}/{exerciseName}")]
        public async Task<IActionResult> GetExerciseStats(Guid userId, string exerciseName)
        {
            var stats = await _service.GetExerciseStatsAsync(userId, exerciseName);
            return Ok(stats);
        }
    }
}
