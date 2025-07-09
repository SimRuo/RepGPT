using Microsoft.AspNetCore.Mvc;
using server.DTOs.PromptHistory;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptHistoryController : ControllerBase
    {
        private readonly PromptHistoryService _service;

        public PromptHistoryController(PromptHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PromptHistoryReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromptHistoryReadDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PromptHistoryReadDto>> Create(PromptHistoryCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
