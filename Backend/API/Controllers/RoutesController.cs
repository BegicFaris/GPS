using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Dtos.RouteDtos;

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
        public async Task<IActionResult> CreateBus(RouteCreateDto routeCreateDto)
        {
            var route = new Data.Models.Route 
            {
            LineId=routeCreateDto.LineId,
            StationId=routeCreateDto.StationId,
            DistanceFromTheNextStation=routeCreateDto.DistanceFromTheNextStation
            }; 
            var createdRoute = await _routeService.CreateRouteAsync(route);
            return CreatedAtAction(nameof(GetRoute), new { id = createdRoute.Id }, createdRoute);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, RouteUpdateDto routeUpdateDto)
        {
            if (id != routeUpdateDto.Id) return BadRequest();
            var existingRoute = await _routeService.GetRouteByIdAsync(id);
            if (existingRoute == null) return NotFound();

            if (routeUpdateDto.LineId != null)
                existingRoute.LineId = routeUpdateDto.LineId.Value;

            if (routeUpdateDto.StationId != null)
                existingRoute.StationId = routeUpdateDto.StationId.Value;

            if (routeUpdateDto.DistanceFromTheNextStation != null)
                existingRoute.DistanceFromTheNextStation = routeUpdateDto.DistanceFromTheNextStation.Value;

            var updatedRoute = await _routeService.UpdateRouteAsync(existingRoute);
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
