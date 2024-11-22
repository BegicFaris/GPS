using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.NotificationDtos
{
    public class NotificationCreateDto
    {
        public required int NotificationTypeId { get; set; }
        public required TimeOnly Duration { get; set; }
        public required DateOnly Date { get; set; }
        public required bool IsActive { get; set; }
        public required int LineId { get; set; }
    }
}
