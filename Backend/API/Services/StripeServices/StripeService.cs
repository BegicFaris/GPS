using GPS.API.Interfaces;
using static GPS.API.Interfaces.IStripeService;
using Stripe;
using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Controllers;

namespace GPS.API.Services.StripeServices
{
    public class StripeService: IStripeService
    {
        private readonly string _apiKey;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StripeService> _logger;

        public StripeService(IConfiguration configuration, ApplicationDbContext context, ILogger<StripeService> logger)
        {
            _apiKey = configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = _apiKey;
            _context = context;
            _logger = logger;
        }

        public async Task<PaymentResult> ProcessPayment(string token, decimal amount)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe uses cents
                Currency = "bam",
                Source = token,
                Description = "Ticket purchase"
            };

            var service = new ChargeService();
            try
            {
                var charge = await service.CreateAsync(options);
                return new PaymentResult { Succeeded = charge.Paid };
            }
            catch (StripeException e)
            {
                return new PaymentResult { Succeeded = false, ErrorMessage = e.Message };
            }
        }

  
    }
}
