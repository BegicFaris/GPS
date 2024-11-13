using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models
{
    public class TicketType
    {
        [Key]
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
