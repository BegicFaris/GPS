namespace GPS.API.Interfaces
{
    public interface IStripeService
    {
        Task<PaymentResult> ProcessPayment(string token, decimal amount, CancellationToken cancellationToken);
        public class PaymentResult
        {
            public bool Succeeded { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
