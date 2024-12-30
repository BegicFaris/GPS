namespace GPS.API.Dtos.TicketDtos
{
    public class TicketCreateRequestDto
    {
        public int TicketInfoId { get; set; }
        public string StripeToken { get; set; }
        public decimal Amount { get; set; }
        public bool SaveCard { get; set; }
        public string Email { get; set; }
    }
}
