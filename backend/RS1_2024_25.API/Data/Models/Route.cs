using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Line))]
        public int LineId { get; set; }
        public Line Line { get; set; }
        [ForeignKey(nameof(Station))]
        public int StationId { get; set; }
        public Station Station { get; set; }
        public float DistanceFromTheNextStation { get; set; }
    }
}
