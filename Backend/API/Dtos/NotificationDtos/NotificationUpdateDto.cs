using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.NotificationDtos
{
    public class NotificationUpdateDto
    {
        public  required int Id {  get; set; }
        public  string? Title { get; set; }
        public  string? Description { get; set; }
        public byte[]? Image { get; set; }
        public  int? NotificationTypeId { get; set; }
        public  int? LineId { get; set; }
        public  DateOnly? CreationDate { get; set; }
        public  int? ManagerId { get; set; }
    }
}
