using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Notification
    {
        [Key]
        public required int Id { get; set; }
        [ForeignKey(nameof(NotificationType))]
        public required int NotificationTypeId { get; set; }
        public NotificationType? NotificationType { get; set; }

        public required TimeOnly Duration { get; set; }
        public required DateOnly Date { get; set; }
        public required bool IsActive { get; set; }
        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }

    }
}
