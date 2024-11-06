using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public int UserId { get; set; }
        public MyAppUser User { get; set; }
        [ForeignKey(nameof(Line))]
        public int LineId { get; set; }
        public Line Line { get; set; }
        [ForeignKey(nameof(Zone))]
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
        [ForeignKey(nameof(TicketType))]
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte[] QrCode { get; set; }
       

    }
}
