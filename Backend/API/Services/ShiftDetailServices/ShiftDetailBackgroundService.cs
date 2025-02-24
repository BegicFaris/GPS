using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.ShiftDetailServices
{
    public class ShiftDetailBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ShiftDetailBackgroundService> _logger;

        public ShiftDetailBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<ShiftDetailBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scheduled Task Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = now.Date.AddHours(8); // Set to your desired time

                if (now > nextRun)
                {
                    // If already passed today’s time, schedule for tomorrow
                    nextRun = nextRun.AddDays(1);
                }

                var delay = nextRun - now;
                _logger.LogInformation($"Next run at {nextRun}. Waiting for {delay}.");

                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    _logger.LogInformation("Scheduled Task Service is stopping.");
                    return;
                }

                if (!stoppingToken.IsCancellationRequested)
                {
                    // Call the method to send emails to all drivers
                    await SendEmailToAllDrivers(stoppingToken);
                }
            }
        }

        private async Task SendEmailToAllDrivers(CancellationToken stoppingToken)
        {
            // Use the service scope to resolve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var shiftDetailService = scope.ServiceProvider.GetRequiredService<IShiftDetailService>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var tenantService = scope.ServiceProvider.GetRequiredService<ICurrentTenantService>();

                var today = DateOnly.FromDateTime(DateTime.Now);

                var tenants = await context.Tenants.ToListAsync(stoppingToken);

                foreach (var tenant in tenants)
                {
                    await tenantService.SetTenant(tenant.Id, stoppingToken);

                    var shifts = await context.Shifts
                        .Include(s => s.Driver)
                        .Where(s => s.ShiftDate.Day == today.Day &&
                                    s.ShiftDate.Month == today.Month &&
                                    s.ShiftDate.Year == today.Year)
                        .ToListAsync(stoppingToken);

                    foreach (var shift in shifts)
                    {
                        if (stoppingToken.IsCancellationRequested)
                            break;

                        string email = "";
                        string subject = $"Shift overview for {today:yyyy-MM-dd}";
                        string message = "Here is your shift overview for today.";

                        if (shift.Driver?.Email == null)
                            continue;

                        email = shift.Driver.Email;
                        try
                        {
                            var pdfBytes = await shiftDetailService.GeneratePdfAsync(shift.Id, stoppingToken);
                            await emailService.SendEmailWithPdfAsync(email, subject, message, pdfBytes, stoppingToken);
                        }
                        catch (ArgumentException ex)
                        {
                            _logger.LogError($"Argument exception while generating PDF for shift {shift.Id}: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Exception while sending email for shift {shift.Id}: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
