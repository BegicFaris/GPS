using GPS.API.Interfaces;
using Stripe;
using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Controllers;
using System.Threading;
using System.Threading.Tasks;
using static GPS.API.Interfaces.IStripeService;
using System.Configuration;

namespace GPS.API.Services.StripeServices
{
    public class StripeService : IStripeService
    {
        private readonly string _apiKey;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StripeService> _logger;

        public StripeService(IConfiguration configuration, ApplicationDbContext context, ILogger<StripeService> logger)
        {
            _apiKey = configuration["Stripe:SecretKey"]??"";
            if (string.IsNullOrWhiteSpace(_apiKey)) { throw new ConfigurationErrorsException("API key is not configured"); }
            StripeConfiguration.ApiKey = _apiKey;
            _context = context;
            _logger = logger;
        }

        public async Task<PaymentResult> ProcessPayment(string token, decimal amount, CancellationToken cancellationToken)
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
                var charge = await service.CreateAsync(options,null,cancellationToken);
                return new PaymentResult { Succeeded = charge.Paid };
            }
            catch (StripeException e)
            {
                return new PaymentResult { Succeeded = false, ErrorMessage = e.Message };
            }
        }
    }
}
