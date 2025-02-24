using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GPS.API.Services.UserServices;

namespace GPS.API.Controllers
{
    public class BusesController(IBusService busService, IMyAppUserService myAppUserService) : MyControllerBase
    {
        private readonly IBusService _busService = busService;
        private readonly IMyAppUserService _myAppUserService = myAppUserService;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllBuses(CancellationToken cancellationToken) =>
            Ok(await _busService.GetAllBusesAsync(cancellationToken));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBus(int id, CancellationToken cancellationToken)
        {
            var bus = await _busService.GetBusByIdAsync(id, cancellationToken);
            if (bus == null) return NotFound();
            return Ok(bus);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateBus(Bus bus, CancellationToken cancellationToken)
        {
            var createdBus = await _busService.CreateBusAsync(bus, cancellationToken);
            return CreatedAtAction(nameof(GetBus), new { id = createdBus.Id }, createdBus);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBus(int id, Bus bus, CancellationToken cancellationToken)
        {
            if (id != bus.Id) return BadRequest();
            var updatedBus = await _busService.UpdateBusAsync(bus, cancellationToken);
            return Ok(updatedBus);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id, CancellationToken cancellationToken)
        {
            var success = await _busService.DeleteBusAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
