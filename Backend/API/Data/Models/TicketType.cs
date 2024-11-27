using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class TicketType : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? TenantId { get; set; }
    }
}
