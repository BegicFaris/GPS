namespace GPS.API.Dtos.StationDtos
{
    public class StationCreateDto
    {
        public required int ZoneId { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string GPSCode { get; set; }
    }
}
