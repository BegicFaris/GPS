using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var ticketTypes = await _ticketTypeService.GetAllAsync(cancellationToken);
            return Ok(ticketTypes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var ticketType = await _ticketTypeService.GetByIdAsync(id, cancellationToken);
            if (ticketType == null) return NotFound();
            return Ok(ticketType);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> Create(TicketType ticketType, CancellationToken cancellationToken)
        {
            var created = await _ticketTypeService.CreateAsync(ticketType, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TicketType ticketType, CancellationToken cancellationToken)
        {
            var updated = await _ticketTypeService.UpdateAsync(id, ticketType, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var success = await _ticketTypeService.DeleteAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
