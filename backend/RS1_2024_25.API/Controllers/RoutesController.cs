using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Controllers
{
    public class RoutesController : MyEndpointBase
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
