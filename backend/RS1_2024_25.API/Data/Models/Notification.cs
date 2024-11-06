using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string NotificationType { get; set; }
        public TimeOnly Duration { get; set; }
        public DateOnly Date { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey(nameof(Line))]
        public int LineId { get; set; }
        public Line Line { get; set; }

    }
}
