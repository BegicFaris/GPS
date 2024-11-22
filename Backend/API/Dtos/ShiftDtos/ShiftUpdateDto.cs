namespace GPS.API.Dtos.ShiftDtos
{
    public class ShiftUpdateDto
    {
        public required int Id { get; set; }
        public int? BusId { get; set; }
        public int? DriverId { get; set; }
        public DateOnly? ShiftDate { get; set; }
        public TimeOnly? ShiftStartingTime { get; set; }
        public TimeOnly? ShiftEndingTime { get; set; }
    }
}
