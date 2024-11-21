using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
