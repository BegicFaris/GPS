﻿using GPS.API.Data.Models;
using GPS.API.Dtos.TicketDtos;
using GPS.API.Interfaces;
using GPS.API.Services.StripeServices;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTickets(CancellationToken cancellationToken, bool includeDeleted = false) =>
            Ok(await _ticketService.GetAllTicketsAsync(cancellationToken,includeDeleted));

        [Authorize]
        [HttpGet("tickets-over-time")]
        public async Task<IActionResult> GetTicketsOverTime(CancellationToken cancellationToken)
        {
            var data = await _ticketService.GetTicketsOverTimeAsync(cancellationToken);
            return Ok(data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id, CancellationToken cancellationToken)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id, cancellationToken);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [Authorize]
        [HttpGet("get/{email}")]
        public async Task<IActionResult> GetTicketsByEmail(string email, CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var tickets = await _ticketService.GetAllTicketsForUserEmail(email, cancellationToken,includeDeleted);
            if (tickets == null || tickets.Count == 0)
            {
                return NoContent();
            }
            return Ok(tickets);
        }

        [Authorize]
        [HttpGet("get/{email}/paginated")]
        public async Task<IActionResult> GetTicketsByEmailPaginated(string email, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, CancellationToken cancellationToken = default )
        {
            var result = await _ticketService.GetUserTicketsPaginatedAsync(email, pageNumber, pageSize, cancellationToken);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketCreateDto ticketCreateDto, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                UserId = ticketCreateDto.UserId,
                TicketInfoId = ticketCreateDto.TicketInfoId,
                CreatedDate = ticketCreateDto.CreatedDate,
                ExpirationDate = ticketCreateDto.ExpirationDate,
                QrCode = ticketCreateDto.QrCode,
            };

            var createdTicket = await _ticketService.CreateTicketAsync(ticket, cancellationToken);
            return CreatedAtAction(nameof(CreateTicket), new { id = createdTicket.Id }, createdTicket);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> TicketUpdate(int id, TicketUpdateDto ticketUpdateDto, CancellationToken cancellationToken)
        {
            if (id != ticketUpdateDto.Id) return BadRequest();

            var existingTicket = await _ticketService.GetTicketByIdAsync(id, cancellationToken);
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

            var updatedTicket = await _ticketService.UpdateTicketAsync(existingTicket, cancellationToken);
            return Ok(updatedTicket);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id, CancellationToken cancellationToken)
        {
            var success = await _ticketService.DeleteTicketAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpPost("create-with-payment")]
        public async Task<ActionResult<Ticket>> CreateTicketWithPayment([FromBody] TicketCreateRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _myAppUserService.GetUserByEmailAsync(request.Email,cancellationToken);
                if (user == null)
                {
                    _logger.LogWarning("User not found when creating ticket");
                    return Unauthorized("User not found");
                }

                _logger.LogInformation($"Processing payment for user: {user.Email}, Amount: {request.Amount}");

                // Process payment with Stripe
                var paymentResult = await _stripeService.ProcessPayment(request.StripeToken, request.Amount, cancellationToken);

                if (!paymentResult.Succeeded)
                {
                    _logger.LogWarning($"Payment failed for user: {user.Email}, Error: {paymentResult.ErrorMessage}");
                    return BadRequest($"Payment failed: {paymentResult.ErrorMessage}");
                }

                _logger.LogInformation($"Payment successful for user: {user.Email}");

                // Create ticket
                var ticket = await _ticketService.CreateTicketOnBuying(request.TicketInfoId, user.Email, cancellationToken);


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
