using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Ticket
    {
        [Key]
        public required int Id { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public required int UserId { get; set; }
        public MyAppUser? User { get; set; }
        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }
        [ForeignKey(nameof(Zone))]
        public required int ZoneId { get; set; }
        public Zone? Zone { get; set; }
        [ForeignKey(nameof(TicketType))]
        public required int TicketTypeId { get; set; }
        public TicketType? TicketType { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required byte[] QrCode { get; set; }
    }
}
