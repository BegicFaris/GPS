using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Notification :IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public byte[]? Image { get; set; }

        [ForeignKey(nameof(NotificationType))]
        public required int NotificationTypeId { get; set; }
        public NotificationType? NotificationType { get; set; }

        [ForeignKey(nameof(Line))]
        public int? LineId { get; set; }
        public Line? Line { get; set; }

        public required DateOnly CreationDate { get; set; }

        [ForeignKey(nameof(MyAppUser))]
        public required int ManagerId { get; set; }
        public MyAppUser? Manager { get; set; }

        public string? TenantId { get; set; }
    }
}
