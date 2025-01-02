using GPS.API.Data.Models;
using GPS.API.Dtos.TicketDtos;
using GPS.API.Interfaces;
using GPS.API.Services.StripeServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TicketsController : MyControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IStripeService _stripeService;
        private readonly IMyAppUserService _myAppUserService;
        private readonly ILogger<TicketsController> _logger;
        public TicketsController(ITicketService ticketService, IStripeService stripeService, IMyAppUserService myAppUserService, ILogger<TicketsController> logger)
        {
            _ticketService = ticketService;
            _stripeService = stripeService;
            _logger = logger;
            _myAppUserService = myAppUserService;
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
                TicketInfoId = ticketCreateDto.TicketInfoId,
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
            if (ticketUpdateDto.TicketInfoId != null)
                existingTicket.TicketInfoId = ticketUpdateDto.TicketInfoId.Value;
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

        [HttpPost("create-with-payment")]
        public async Task<ActionResult<Ticket>> CreateTicketWithPayment([FromBody] TicketCreateRequestDto request)
        {
            try
            {
                var user = await _myAppUserService.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    _logger.LogWarning("User not found when creating ticket");
                    return Unauthorized("User not found");
                }

                _logger.LogInformation($"Processing payment for user: {user.Email}, Amount: {request.Amount}");

                // Process payment with Stripe
                var paymentResult = await _stripeService.ProcessPayment(request.StripeToken, request.Amount);

                if (!paymentResult.Succeeded)
                {
                    _logger.LogWarning($"Payment failed for user: {user.Email}, Error: {paymentResult.ErrorMessage}");
                    return BadRequest($"Payment failed: {paymentResult.ErrorMessage}");
                }

                _logger.LogInformation($"Payment successful for user: {user.Email}");

                // Create ticket
                var ticket = await _ticketService.CreateTicketOnBuying(request.TicketInfoId, user.Email);


                _logger.LogInformation($"Ticket created successfully for user: {user.Email}, TicketId: {ticket.Id}");
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the ticket");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
