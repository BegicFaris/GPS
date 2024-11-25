using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Zone : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? TenantId { get; set; }
    }
}
