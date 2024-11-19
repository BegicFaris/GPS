using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Controllers
{
    public class RoutesController : MyControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoutes() =>
            Ok(await _routeService.GetAllRoutesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoute(int id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);
            if (route == null) return NotFound();
            return Ok(route);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBus(Data.Models.Route route)
        {
            var createdRoute = await _routeService.CreateRouteAsync(route);
            return CreatedAtAction(nameof(GetRoute), new { id = createdRoute.Id }, createdRoute);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, Data.Models.Route route)
        {
            if (id != route.Id) return BadRequest();
            var updatedRoute = await _routeService.UpdateRouteAsync(route);
            return Ok(updatedRoute);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var success = await _routeService.DeleteRouteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
