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
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            return Ok(passengers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);
            if (passenger == null)
                return NotFound();
            return Ok(passenger);
        }

        [HttpPost]
        public async Task<ActionResult<Passenger>> CreatePassenger(Passenger passenger)
        {
            var createdPassenger = await _passengerService.CreatePassengerAsync(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = createdPassenger.Id }, createdPassenger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, Passenger passenger)
        {
            if (id != passenger.Id) return BadRequest();
            var existingPassenger = await _passengerService.GetPassengerByIdAsync(id);

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




            var updatedPassenger = await _passengerService.UpdatePassengerAsync(existingPassenger);
            if (updatedPassenger == null)
                return NotFound();

            return Ok(updatedPassenger);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            var result = await _passengerService.DeletePassengerAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
