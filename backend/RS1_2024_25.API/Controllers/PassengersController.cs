using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Controllers
{
    public class PassengersController: MyEndpointBase
    {
        private readonly IPassengerService _passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            return Ok(passengers);
        }

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
            if (id != passenger.Id)
                return BadRequest();

            var updatedPassenger = await _passengerService.UpdatePassengerAsync(passenger);
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
