using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TicketTypeController : MyControllerBase
    {
        private readonly ITicketTypeService _ticketTypeService;

        public TicketTypeController(ITicketTypeService ticketTypeService)
        {
            _ticketTypeService = ticketTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ticketTypes = await _ticketTypeService.GetAllAsync();
            return Ok(ticketTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticketType = await _ticketTypeService.GetByIdAsync(id);
            if (ticketType == null) return NotFound();
            return Ok(ticketType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketType ticketType)
        {
            var created = await _ticketTypeService.CreateAsync(ticketType);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TicketType ticketType)
        {
            var updated = await _ticketTypeService.UpdateAsync(id, ticketType);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ticketTypeService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
