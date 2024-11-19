using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Bus
    {
        [Key]
        public int Id { get; set; }
        public required string RegistrationNumber { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public required string Capacity { get; set; }
        public required string ManufactureYear { get; set; }
    }
}
