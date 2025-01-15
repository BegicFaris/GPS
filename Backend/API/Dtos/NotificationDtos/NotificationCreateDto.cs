using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.NotificationDtos
{
    public class NotificationCreateDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public byte[]? Image { get; set; }
        public required int NotificationTypeId { get; set; }
        public int? LineId { get; set; }
        public required DateTime CreationDate { get; set; }
        public required int ManagerId { get; set; }
    }
}
