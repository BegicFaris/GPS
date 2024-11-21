using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TicketController : MyControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets() =>
            Ok(await _ticketService.GetAllTicketsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            var createdTicket = await _ticketService.CreateTicketAsync(ticket);
            return CreatedAtAction(nameof(CreateTicket), new { id = createdTicket.Id }, createdTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id) return BadRequest();
            var updatedTicket = await _ticketService.UpdateTicketAsync(ticket);
            return Ok(updatedTicket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var success = await _ticketService.DeleteTicketAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
