namespace GPS.API.Dtos.TicketDtos
{
    public class TicketHistoryDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }
        public int TicketInfoId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
