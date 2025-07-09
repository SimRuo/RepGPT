using Microsoft.AspNetCore.Mvc;
using server.DTOs.WorkoutLog;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutLogsController : ControllerBase
    {
        private readonly WorkoutLogService _workoutLogService;

        public WorkoutLogsController(WorkoutLogService workoutLogService)
        {
            _workoutLogService = workoutLogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutLogReadDto>>> GetAll()
        {
            var logs = await _workoutLogService.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutLogReadDto>> GetById(int id)
        {
            var log = await _workoutLogService.GetByIdAsync(id);
            if (log == null) return NotFound();
            return Ok(log);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutLogReadDto>> Create(WorkoutLogCreateDto dto)
        {
            var created = await _workoutLogService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkoutLogUpdateDto dto)
        {
            var success = await _workoutLogService.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _workoutLogService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
