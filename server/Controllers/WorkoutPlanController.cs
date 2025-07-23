using Microsoft.AspNetCore.Mvc;
using server.DTOs.GPT;
using server.DTOs.WorkoutPlan;
using server.Services;
using server.Services.Ai;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutPlansController : ControllerBase
    {
        private readonly WorkoutPlanService _service;
        private readonly GptService _gptService;

        public WorkoutPlansController(WorkoutPlanService service, GptService gptService)
        {
            _service = service;
            _gptService = gptService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutPlanReadDto>>> GetAll()
        {
            var plans = await _service.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutPlanReadDto>> GetById(int id)
        {
            var plan = await _service.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutPlanReadDto>> Create(WorkoutPlanCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkoutPlanUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost("generate-only")]
        public async Task<ActionResult<string>> GenerateOnly([FromBody] PromptRequestDto dto)
        {
            try
            {
                var result = await _gptService.SendPromptAsync(dto.Prompt);
                return Ok(result); // raw JSON string
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutPlanReadDto>>> GetByUserId(int userId)
        {
            var plans = await _service.GetByUserIdAsync(userId);
            return Ok(plans);
        }
        [HttpPost("{id}/progress")]
        public async Task<IActionResult> ApplyProgression(int id)
        {
            var updated = await _service.ApplyProgressiveOverloadAsync(id);
            return updated ? Ok() : NotFound();
        }
    }
}
