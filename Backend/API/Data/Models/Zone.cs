using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Zone
    {
        [Key]
        public required int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
