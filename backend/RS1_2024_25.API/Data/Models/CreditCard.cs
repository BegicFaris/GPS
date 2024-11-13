namespace RS1_2024_25.API.Data.Models
{
    public class CreditCard
    {
        public required int Id { get; set; }
        public required int CardNumber { get; set; }
        public required int ExpirationDate { get; set; }
        public required string CardName { get; set; }
        public required int CCV { get; set; }
    }
}
