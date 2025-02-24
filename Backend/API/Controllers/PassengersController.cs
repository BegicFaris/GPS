using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GPS.API.Services.DriverServices;

namespace GPS.API.Controllers
{
    public class PassengersController: MyControllerBase
    {
        private readonly IPassengerService _passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers(CancellationToken cancellationToken)
        {
            var passengers = await _passengerService.GetAllPassengersAsync(cancellationToken);
            return Ok(passengers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id, CancellationToken cancellationToken)
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id, cancellationToken);
            if (passenger == null)
                return NotFound();
            return Ok(passenger);
        }


        [HttpPost]
        public async Task<ActionResult<Passenger>> CreatePassenger(Passenger passenger, CancellationToken cancellationToken)
        {
            var createdPassenger = await _passengerService.CreatePassengerAsync(passenger, cancellationToken);
            return CreatedAtAction(nameof(GetPassenger), new { id = createdPassenger.Id }, createdPassenger);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, Passenger passenger, CancellationToken cancellationToken)
        {
            if (id != passenger.Id) return BadRequest();
            var existingPassenger = await _passengerService.GetPassengerByIdAsync(id, cancellationToken);
            if(existingPassenger == null) return NotFound();
            if (passenger.FirstName != null)
                existingPassenger.FirstName = passenger.FirstName;

            if (passenger.LastName != null)
                existingPassenger.LastName = passenger.LastName;

            if (passenger.Email != null)
                existingPassenger.Email = passenger.Email;

            if (passenger.BirthDate.HasValue)
                existingPassenger.BirthDate = passenger.BirthDate;

            if (passenger.RegistrationDate.HasValue)
                existingPassenger.RegistrationDate = passenger.RegistrationDate;

            if (passenger.Image != null)
                existingPassenger.Image = passenger.Image;

            if (passenger.Address != null)
                existingPassenger.Address = passenger.Address;

            if (passenger.Status.HasValue)
                existingPassenger.Status = passenger.Status;
            if (passenger.DiscountID != null)
                existingPassenger.DiscountID = passenger.DiscountID;

            existingPassenger.TwoFactorEnabled = passenger.TwoFactorEnabled;



            var updatedPassenger = await _passengerService.UpdatePassengerAsync(existingPassenger, cancellationToken);
            if (updatedPassenger == null)
                return NotFound();

            return Ok(updatedPassenger);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id, CancellationToken cancellationToken)
        {
            var result = await _passengerService.DeletePassengerAsync(id, cancellationToken);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
