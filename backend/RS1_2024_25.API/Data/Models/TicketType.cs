using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
