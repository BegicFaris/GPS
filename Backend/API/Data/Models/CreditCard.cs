using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class CreditCard : IMustHaveTenant
    {
        public required int Id { get; set; }
        public required string CardNumber { get; set; }
        public required string ExpirationDate { get; set; }
        public required string CardName { get; set; }
        public required int CCV { get; set; }
        public string? TenantId { get; set; }
    }
}
