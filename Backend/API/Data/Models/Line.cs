using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Line : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [ForeignKey(nameof(Station))]
        public required int StartingStationId { get; set; }
        public Station? StartingStation { get; set; }
        [ForeignKey(nameof(Station))]
        public required int EndingStationId { get; set; }
        public Station? EndingStation { get; set; }
        public required string CompleteDistance { get; set; }
        public required bool IsActive { get; set; }
        public string? TenantId { get; set; }
    }
}
