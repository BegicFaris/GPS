namespace GPS.API.Dtos.TicketDtos
{
    public class TicketCreateDto
    {
        public required int UserId { get; set; }
        public required int TicketInfoId { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required byte[] QrCode { get; set; }
    }
}
