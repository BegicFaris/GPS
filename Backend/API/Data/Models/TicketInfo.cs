using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Data.Models
{
    public class TicketInfo:IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public required decimal  Price { get; set; }
        [ForeignKey(nameof(Zone))]
        public required int ZoneId { get; set; }
        public Zone? Zone { get; set; }
        [ForeignKey(nameof(TicketType))]
        public required int TicketTypeId { get; set; }
        public TicketType? TicketType { get; set; }
        public string? TenantId { get; set; }

    }
}
