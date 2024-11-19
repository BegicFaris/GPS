using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Line
    {
        [Key]
        public required int Id { get; set; }
        public required string Name { get; set; }
        [ForeignKey(nameof(Station))]
        public required int StartingStationID { get; set; }
        public Station? StartingStation { get; set; }
        [ForeignKey(nameof(Station))]
        public required int EndingStationID { get; set; }
        public Station? EndingStation { get; set; }
        public required string CompleteDistance { get; set; }
        public required bool IsActive { get; set; }
    }
}
