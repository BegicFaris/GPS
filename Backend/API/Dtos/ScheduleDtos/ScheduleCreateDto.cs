using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.ScheduleDtos
{
    public class ScheduleCreateDto
    {
        public required int LineId { get; set; }
        public required TimeOnly DepartureTime { get; set; }
    }
}
