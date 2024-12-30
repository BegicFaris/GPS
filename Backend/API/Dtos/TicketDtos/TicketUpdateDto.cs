namespace GPS.API.Dtos.TicketDtos
{
    public class TicketUpdateDto
    {
        public required int Id { get; set; }
        public int? UserId { get; set; }
        public int? TicketInfoId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public byte[]? QrCode { get; set; }
    }
}
