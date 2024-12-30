using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TicketInfoController:MyControllerBase
    {
        private readonly ITicketInfoService _service;

        public TicketInfoController(ITicketInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketInfo>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketInfo>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TicketInfo>> Create(TicketInfo ticketInfo)
        {
            var created = await _service.CreateAsync(ticketInfo);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TicketInfo>> Update(int id, TicketInfo ticketInfo)
        {
            var updated = await _service.UpdateAsync(id, ticketInfo);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        [HttpGet("by-ticket-type/{ticketTypeId}")]
        public async Task<ActionResult<IEnumerable<TicketInfo>>> GetByTicketTypeId(int ticketTypeId)
        {
            var result = await _service.GetByTicketTypeIdAsync(ticketTypeId);
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
        }
    }
}
