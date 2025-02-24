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
        public async Task<IActionResult> GetAllZones(CancellationToken cancellationToken) =>
            Ok(await _zoneService.GetAllZonesAsync(cancellationToken));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetZone(int id, CancellationToken cancellationToken)
        {
            var zone = await _zoneService.GetZoneByIdAsync(id, cancellationToken);
            if (zone == null) return NotFound();
            return Ok(zone);
        }
    }
}
