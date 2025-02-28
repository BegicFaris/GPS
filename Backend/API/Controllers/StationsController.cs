using GPS.API.Data.Models;
using GPS.API.Dtos.StationDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{

    public class StationsController : MyControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStations(CancellationToken cancellationToken) =>
            Ok(await _stationService.GetAllStationsAsync(cancellationToken));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStation(int id, CancellationToken cancellationToken)
        {
            var station = await _stationService.GetStationByIdAsync(id, cancellationToken);
            if (station == null) return NotFound();
            return Ok(station);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateStation(StationCreateDto stationCreateDto, CancellationToken cancellationToken)
        {
            var station = new Station
            {
                ZoneId = stationCreateDto.ZoneId,
                Name = stationCreateDto.Name,
                Location = stationCreateDto.Location,
                GPSCode = stationCreateDto.GPSCode,
            };

            var createdStation = await _stationService.CreateStationAsync(station, cancellationToken);
            return CreatedAtAction(nameof(CreateStation), new { id = createdStation.Id }, createdStation);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> StationUpdate(int id, StationUpdateDto stationUpdateDto, CancellationToken cancellationToken)
        {

            if (id != stationUpdateDto.Id) return BadRequest();

            var existingStation = await _stationService.GetStationByIdAsync(id, cancellationToken);
            if (existingStation == null) return NotFound($"Shift with Id:{id} not found!");

            if (stationUpdateDto.ZoneId != null)
                existingStation.ZoneId = stationUpdateDto.ZoneId.Value;
            if (!string.IsNullOrEmpty(stationUpdateDto.Name))
                existingStation.Name = stationUpdateDto.Name;
            if (!string.IsNullOrEmpty(stationUpdateDto.Location))
                existingStation.Location = stationUpdateDto.Location;
            if (!string.IsNullOrEmpty(stationUpdateDto.GPSCode))
                existingStation.GPSCode = stationUpdateDto.GPSCode;

            var updatedStation = await _stationService.UpdateStationAsync(existingStation, cancellationToken);
            return Ok(updatedStation);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _stationService.DeleteStationAsync(id, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
