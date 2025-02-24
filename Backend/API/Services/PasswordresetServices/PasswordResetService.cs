using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace GPS.API.Services.PasswordresetServices
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public PasswordResetService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task GenerateResetCode(string email, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var code = new Random().Next(100000, 999999).ToString();
            user.ResetCode = code;
            user.ResetCodeExpiration = DateTime.UtcNow.AddMinutes(15);

            await _context.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(email, "Password Reset Code", $"Your password reset code is: {code}", cancellationToken);
        }

        public async Task<bool> VerifyResetCode(string email, string code, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user == null)
            {
                return false;
            }

            return user.ResetCode == code && user.ResetCodeExpiration > DateTime.UtcNow;
        }

        public async Task<bool> ResetPassword(string email, string code, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user == null || user.ResetCode != code || user.ResetCodeExpiration <= DateTime.UtcNow)
            {
                return false;
            }

            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            user.PasswordSalt = hmac.Key;

            user.ResetCode = null;
            user.ResetCodeExpiration = null;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
