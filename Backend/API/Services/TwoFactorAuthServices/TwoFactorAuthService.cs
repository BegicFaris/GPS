using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GPS.API.Services.TwoFactorAuthServices
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public TwoFactorAuthService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<bool> IsUsingTwoFactorAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user?.TwoFactorEnabled ?? false;
        }

        public async Task GenerateTwoFactorCodeAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var code = new Random().Next(100000, 999999).ToString();
            user.TwoFactorCode = code;
            user.TwoFactorCodeExpiry = DateTime.UtcNow.AddMinutes(15);

            await _context.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(email, "Two-factor authentication", $"Your 2FA verification code is: {code}", cancellationToken);
        }

        public async Task<bool> VerifyTwoFactorCodeAsync(string email, string code, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user?.TwoFactorCode == code && user.TwoFactorCodeExpiry > DateTime.UtcNow;
        }
    }
}
