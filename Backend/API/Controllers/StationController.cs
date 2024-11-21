using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{

    public class StationController : MyControllerBase
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStations() =>
            Ok(await _stationService.GetAllStationsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStation(int id)
        {
            var station = await _stationService.GetStationByIdAsync(id);
            if (station == null) return NotFound();
            return Ok(station);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStation(Station station)
        {
            var createdStation = await _stationService.CreateStationAsync(station);
            return CreatedAtAction(nameof(CreateStation), new { id = createdStation.Id }, createdStation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStation(int id, Station station)
        {
            if (id != station.Id) return BadRequest();
            var updatedStation = await _stationService.UpdateStationAsync(station);
            return Ok(updatedStation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var success = await _stationService.DeleteStationAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
