namespace GPS.API.Dtos.ShiftDtos
{
    public class ShiftCreateDto
    {
        public required int BusId { get; set; }
        public required int DriverId { get; set; }
        public required DateOnly ShiftDate { get; set; }
        public required TimeOnly ShiftStartingTime { get; set; }
        public required TimeOnly ShiftEndingTime { get; set; }
    }
}
