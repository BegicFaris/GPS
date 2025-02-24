using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace GPS.API.Controllers
{
    public class TicketInfoController:MyControllerBase
    {
        private readonly ITicketInfoService _service;

        public TicketInfoController(ITicketInfoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketInfo>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketInfo>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

       
        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<ActionResult<TicketInfo>> Create(TicketInfo ticketInfo, CancellationToken cancellationToken)
        {
            var created = await _service.CreateAsync(ticketInfo, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<ActionResult<TicketInfo>> Update(int id, TicketInfo ticketInfo, CancellationToken cancellationToken)
        {
            var updated = await _service.UpdateAsync(id, ticketInfo, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var success = await _service.DeleteAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
        [HttpGet("by-ticket-type/{ticketTypeId}")]
        public async Task<ActionResult<IEnumerable<TicketInfo>>> GetByTicketTypeId(int ticketTypeId, CancellationToken cancellationToken)
        {
            var result = await _service.GetByTicketTypeIdAsync(ticketTypeId, cancellationToken);
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
        }
    }
}
