using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Notification :IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }

        public required string Description { get; set; }

        [ForeignKey(nameof(NotificationType))]
        public required int NotificationTypeId { get; set; }
        public NotificationType? NotificationType { get; set; }

        public required TimeOnly Duration { get; set; }
        public required DateOnly Date { get; set; }
        public required bool IsActive { get; set; }
        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }
        public string? TenantId { get; set; }
    }
}
