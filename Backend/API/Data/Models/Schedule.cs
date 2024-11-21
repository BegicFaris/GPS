using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Data.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }
        public required TimeOnly DepartureTime { get; set; }
    }
}
