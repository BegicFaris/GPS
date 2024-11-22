using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.ScheduleDtos
{
    public class ScheduleUpdateDto
    {
        public required int Id { get; set; }
        public int? LineId { get; set; }
        public TimeOnly? DepartureTime { get; set; }
    }
}
