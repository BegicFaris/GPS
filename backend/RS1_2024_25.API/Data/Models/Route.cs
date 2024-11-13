using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Route
    {
        [Key]
        public required int Id { get; set; }
        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }
        [ForeignKey(nameof(Station))]
        public required int StationId { get; set; }
        public Station? Station { get; set; }
        public required float DistanceFromTheNextStation { get; set; }
    }
}
