using Microsoft.AspNetCore.Mvc;
using server.DTOs.WorkoutExercise;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly WorkoutExerciseService _service;

        public WorkoutExercisesController(WorkoutExerciseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkoutExerciseReadDto>>> GetAll()
        {
            var exercises = await _service.GetAllAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExerciseReadDto>> GetById(int id)
        {
            var exercise = await _service.GetByIdAsync(id);
            if (exercise == null)
                return NotFound();
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutExerciseReadDto>> Create(WorkoutExerciseCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkoutExerciseUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
