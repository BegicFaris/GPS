namespace GPS.API.Dtos.ShiftDtos
{
    public class ShiftDetailsDto
    {
        public ShiftDto? Shift { get; set; }
        public List<ShiftDetailItemDTO> Details { get; set; } = new List<ShiftDetailItemDTO>();
    }
    public class ShiftDetailItemDTO
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string? LineName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
