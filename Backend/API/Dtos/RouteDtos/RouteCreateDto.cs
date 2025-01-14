using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.RouteDtos
{
    public class RouteCreateDto
    {
        public required int LineId { get; set; }
        public required int StationId { get; set; }
        public required TimeOnly DistanceFromTheNextStation { get; set; }
    }
}
