using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class ZonesController:MyControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZonesController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllZones() =>
            Ok(await _zoneService.GetAllZonesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetZone(int id)
        {
            var zone = await _zoneService.GetZoneByIdAsync(id);
            if (zone == null) return NotFound();
            return Ok(zone);
        }
    }
}
