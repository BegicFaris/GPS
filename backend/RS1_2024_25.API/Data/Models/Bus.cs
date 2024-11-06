using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Bus
    {
        [Key]
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacturer {  get; set; }
        public string Model { get; set; } 
        public string Capacity { get; set; }
        public string ManufactureYear { get; set; }
    }
}
