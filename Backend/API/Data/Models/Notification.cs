using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
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
