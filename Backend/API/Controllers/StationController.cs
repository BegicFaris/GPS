using GPS.API.Data.Models;
using GPS.API.Dtos.StationDtos;
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
        public async Task<IActionResult> CreateStation(StationCreateDto stationCreateDto)
        {
            var station = new Station
            {
                ZoneId = stationCreateDto.ZoneId,
                Name = stationCreateDto.Name,
                Location = stationCreateDto.Location,
                GPSCode = stationCreateDto.GPSCode,
            };

            var createdStation = await _stationService.CreateStationAsync(station);
            return CreatedAtAction(nameof(CreateStation), new { id = createdStation.Id }, createdStation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> StationUpdate(int id, StationUpdateDto stationUpdateDto)
        {
            if (id != stationUpdateDto.Id) return BadRequest();

            var existingStation = await _stationService.GetStationByIdAsync(id);
            if (existingStation == null) return NotFound($"Shift with Id:{id} not found!");

            if (stationUpdateDto.ZoneId != null)
                existingStation.ZoneId = stationUpdateDto.ZoneId.Value;
            if (!string.IsNullOrEmpty(stationUpdateDto.Name))
                existingStation.Name = stationUpdateDto.Name;
            if (!string.IsNullOrEmpty(stationUpdateDto.Location))
                existingStation.Location = stationUpdateDto.Location;
            if (!string.IsNullOrEmpty(stationUpdateDto.GPSCode))
                existingStation.GPSCode = stationUpdateDto.GPSCode;

            var updatedStation = await _stationService.UpdateStationAsync(existingStation);
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
