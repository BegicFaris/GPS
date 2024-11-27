namespace GPS.API.Dtos.StationDtos
{
    public class StationUpdateDto
    {
        public required int Id { get; set; }
        public int? ZoneId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? GPSCode { get; set; }
    }
}
