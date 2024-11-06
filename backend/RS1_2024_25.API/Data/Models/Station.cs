using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Zone))]
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string GPSCode { get; set; }
    }
}
