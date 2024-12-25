using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GPS.API.Services.TwoFactorAuthServices
{
    public class TwoFactorAuthService: ITwoFactorAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public TwoFactorAuthService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<bool> IsUsingTwoFactor(string email)
        {
            var user =  await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user.TwoFactorEnabled;
            }
            return false;
        }

        public async Task GenerateTwoFactorCode(string email)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var code = new Random().Next(100000, 999999).ToString();
            user.TwoFactorCode = code;
            user.TwoFactorCodeExpiry = DateTime.UtcNow.AddMinutes(15);

            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(email, "Two factor authentification", $"Your 2FA verification code is: {code}");
        }

        public async Task<bool> VerifyTwoFactorCode(string email, string code)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }

            return user.TwoFactorCode == code && user.TwoFactorCodeExpiry > DateTime.UtcNow;
        }

    }
}
