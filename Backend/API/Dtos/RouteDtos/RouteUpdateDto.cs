using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Dtos.RouteDtos
{
    public class RouteUpdateDto
    {
        public required int Id { get; set; }
        public int? LineId { get; set; }
        public int? StationId { get; set; }
        public TimeOnly? DistanceFromTheNextStation { get; set; }
    }
}
