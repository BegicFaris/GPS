namespace GPS.API.Dtos.ShiftDtos
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string? BusNumber { get; set; }
        public int DriverId { get; set; }
        public string? DriverName { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan ShiftStartingTime { get; set; }
        public TimeSpan ShiftEndingTime { get; set; }
        public string? Status { get; set; } // "Ended", "Current", "Upcoming"
    }
}
