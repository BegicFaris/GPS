using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Ticket : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public required int UserId { get; set; }
        public MyAppUser? User { get; set; }
        public required int TicketInfoId { get; set; }
        public TicketInfo? TicketInfo { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required byte[] QrCode { get; set; }
        public string? TenantId { get; set; }
    }
}
