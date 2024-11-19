using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Controllers
{
    public class BusesController : MyControllerBase
    {
        private readonly IBusService _busService;

        public BusesController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBuses() =>
            Ok(await _busService.GetAllBusesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBus(int id)
        {
            var bus = await _busService.GetBusByIdAsync(id);
            if (bus == null) return NotFound();
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBus(Bus bus)
        {
            var createdBus = await _busService.CreateBusAsync(bus);
            return CreatedAtAction(nameof(GetBus), new { id = createdBus.Id }, createdBus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBus(int id, Bus bus)
        {
            if (id != bus.Id) return BadRequest();
            var updatedBus = await _busService.UpdateBusAsync(bus);
            return Ok(updatedBus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var success = await _busService.DeleteBusAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
