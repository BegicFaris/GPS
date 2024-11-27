using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Station:IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Zone))]
        public required int ZoneId { get; set; }
        public Zone? Zone { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string GPSCode { get; set; }
        public string? TenantId { get; set; }
    }
}
