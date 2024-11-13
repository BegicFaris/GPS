using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Bus 
    {
        [Key]
        public required int Id { get; set; }
        public required string RegistrationNumber { get; set; }
        public required string Manufacturer {  get; set; }
        public required string Model { get; set; } 
        public required string Capacity { get; set; }
        public required string ManufactureYear { get; set; }
        public string? TenantId { get; set; }
    }
}
