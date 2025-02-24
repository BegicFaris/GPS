using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Dtos.RouteDtos;
using Microsoft.AspNetCore.Authorization;

namespace GPS.API.Controllers
{
    public class RoutesController : MyControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllRoutes(CancellationToken cancellationToken) =>
            Ok(await _routeService.GetAllRoutesAsync(cancellationToken));


        [Authorize]
        [HttpGet("line/{lineId}")]
        public async Task<IActionResult> GetAllRoutesByLineId(int lineId, CancellationToken cancellationToken)
        {
            return Ok(await _routeService.GetAllRoutesByLineIdAsync(lineId, cancellationToken));
        }


        [Authorize]
        [HttpGet("station/{lineId}")]
        public async Task<IActionResult> GetStationCountByLineIdAsync(int lineId, CancellationToken cancellationToken)
        {
            var count = await _routeService.GetStationCountByLineIdAsync(lineId, cancellationToken);
            return Ok(new { count });
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("line/{lineId}")]
        public async Task<IActionResult> DeleteAllRoutesByLineIdAsync(int lineId, CancellationToken cancellationToken)
        {
            var success = await _routeService.DeleteAllRoutesByLineIdAsync(lineId, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoute(int id, CancellationToken cancellationToken)
        {
            var route = await _routeService.GetRouteByIdAsync(id, cancellationToken);
            if (route == null) return NotFound();
            return Ok(route);
        }
        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateRoute(RouteCreateDto routeCreateDto, CancellationToken cancellationToken)
        {
            var route = new Data.Models.Route
            {
                LineId = routeCreateDto.LineId,
                StationId = routeCreateDto.StationId,
                DistanceFromTheNextStation = routeCreateDto.DistanceFromTheNextStation,
                Order = routeCreateDto.Order,
            };
            var createdRoute = await _routeService.CreateRouteAsync(route, cancellationToken);
            return CreatedAtAction(nameof(GetRoute), new { id = createdRoute.Id }, createdRoute);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, RouteUpdateDto routeUpdateDto, CancellationToken cancellationToken)
        {
            if (id != routeUpdateDto.Id) return BadRequest();
            var existingRoute = await _routeService.GetRouteByIdAsync(id,cancellationToken);
            if (existingRoute == null) return NotFound($"Route with Id:{id} not found!");

            if (routeUpdateDto.LineId != null)
                existingRoute.LineId = routeUpdateDto.LineId.Value;

            if (routeUpdateDto.StationId != null)
                existingRoute.StationId = routeUpdateDto.StationId.Value;

            if (routeUpdateDto.DistanceFromTheNextStation != null)
                existingRoute.DistanceFromTheNextStation = routeUpdateDto.DistanceFromTheNextStation.Value;

            var updatedRoute = await _routeService.UpdateRouteAsync(existingRoute, cancellationToken);
            return Ok(updatedRoute);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id, CancellationToken cancellationToken)
        {
            var success = await _routeService.DeleteRouteAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
