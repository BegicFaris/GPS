namespace GPS.API.Dtos.TicketDtos
{
    public class TicketHistoryDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int TicketTypeId { get; set; }
        public string TicketTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
