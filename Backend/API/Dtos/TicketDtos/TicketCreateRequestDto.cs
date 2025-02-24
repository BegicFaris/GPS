namespace GPS.API.Dtos.TicketDtos
{
    public class TicketCreateRequestDto
    {
        public int TicketInfoId { get; set; }
        public required string StripeToken { get; set; }
        public decimal Amount { get; set; }
        public bool SaveCard { get; set; }
        public required string Email { get; set; }
    }
}
