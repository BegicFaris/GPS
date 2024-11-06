namespace RS1_2024_25.API.Data.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public int ExpirationDate { get; set; }
        public string CardName { get; set; }
        public int CCV { get; set; }
    }
}
