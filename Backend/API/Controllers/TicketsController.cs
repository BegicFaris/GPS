using GPS.API.Data.Models;
using GPS.API.Dtos.TicketDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TicketsController : MyControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
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
        [HttpGet("get/{email}")]
          public async Task<IActionResult> GetTicketsByEmail(string email)
          {
         
                  var tickets = await _ticketService.GetAllTicketsForUserEmail(email);

                  if (tickets == null || !tickets.Any())
                  {
                return NoContent();
            }

                  return Ok(tickets);
       
          }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketCreateDto ticketCreateDto)
        {
            var ticket = new Ticket
            {
                UserId = ticketCreateDto.UserId,
                LineId = ticketCreateDto.LineId,
                ZoneId = ticketCreateDto.ZoneId,
                TicketTypeId = ticketCreateDto.TicketTypeId,
                CreatedDate = ticketCreateDto.CreatedDate,
                ExpirationDate = ticketCreateDto.ExpirationDate,
                QrCode = ticketCreateDto.QrCode,
            };

            var createdTicket = await _ticketService.CreateTicketAsync(ticket);
            return CreatedAtAction(nameof(CreateTicket), new { id = createdTicket.Id }, createdTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> TicketUpdate(int id, TicketUpdateDto ticketUpdateDto)
        {
            if (id != ticketUpdateDto.Id) return BadRequest();

            var existingTicket = await _ticketService.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound(new { Message = $"Ticket with Id {id} not found." });
            }

            if (ticketUpdateDto.UserId != null)
                existingTicket.UserId = ticketUpdateDto.UserId.Value;
            if (ticketUpdateDto.LineId != null)
                existingTicket.LineId = ticketUpdateDto.LineId.Value;
            if (ticketUpdateDto.ZoneId != null)
                existingTicket.ZoneId = ticketUpdateDto.ZoneId.Value;
            if (ticketUpdateDto.TicketTypeId != null)
                existingTicket.TicketTypeId = ticketUpdateDto.TicketTypeId.Value;
            if (ticketUpdateDto.CreatedDate != null)
                existingTicket.CreatedDate = ticketUpdateDto.CreatedDate.Value;
            if (ticketUpdateDto.ExpirationDate != null)
                existingTicket.ExpirationDate = ticketUpdateDto.ExpirationDate.Value;
            if (ticketUpdateDto.QrCode != null)
                existingTicket.QrCode = ticketUpdateDto.QrCode;

            var updatedTicket = await _ticketService.UpdateTicketAsync(existingTicket);
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
