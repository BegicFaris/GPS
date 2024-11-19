using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Station
    {
        [Key]
        public required int Id { get; set; }
        [ForeignKey(nameof(Zone))]
        public required int ZoneId { get; set; }
        public Zone? Zone { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string GPSCode { get; set; }
    }
}
