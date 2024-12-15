namespace GPS.API.Dtos.NotificationDtos
{
    public class NotificationUpdateDto
    {
        public required int Id {  get; set; }
        
        public string? Description { get; set; }
        public  int? NotificationTypeId { get; set; }
        public  TimeOnly? Duration { get; set; }
        public  DateOnly? Date { get; set; }
        public  bool? IsActive { get; set; }
        public int? LineId { get; set; }
    }
}
