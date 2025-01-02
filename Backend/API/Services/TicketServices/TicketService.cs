using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace GPS.API.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketService> _logger;
        public TicketService(ApplicationDbContext context, ILogger<TicketService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync() =>
            await _context.Tickets.Include(x => x.TicketInfo ).Include(x => x.User).ToListAsync();
        public async Task<Ticket> GetTicketByIdAsync(int id) =>
          await _context.Tickets.Include(x => x.TicketInfo).Include(x=>x.User).SingleOrDefaultAsync(x => x.Id == id);
        public async Task<List<Ticket>> GetAllTicketsForUserEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            return await _context.Tickets.Include(x => x.TicketInfo).Include(x => x.User).Include(x => x.TicketInfo.Zone).Include(x => x.TicketInfo.TicketType)
                .Where(t => t.User.Email == email)
                .ToListAsync(); 
        }
        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }


        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Ticket> CreateTicketOnBuying(int ticketInfoId, string userEmail)
        {
            _logger.LogInformation($"Creating ticket for user: {userEmail}, TicketInfoId: {ticketInfoId}");
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                _logger.LogError($"User not found: {userEmail}");
                throw new Exception("User not found");
            }

            var ticketInfo = await _context.TicketInfos.Include(ti => ti.TicketType).FirstOrDefaultAsync(ti => ti.Id == ticketInfoId);
            if (ticketInfo == null)
            {
                _logger.LogError($"Ticket info not found: {ticketInfoId}");
                throw new Exception("Ticket info not found");
            }
            int expirationDays;
            if (ticketInfo.TicketType.Name == "Monthly")
            {
                expirationDays = 30;
            }
            else {
                expirationDays = 1;
            }
            var ticket = new Ticket
            {
                UserId = user.Id,
                TicketInfoId = ticketInfoId,
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(expirationDays), // Adjust as needed
                QrCode = GenerateQrCode() // Implement this method
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Ticket created successfully. TicketId: {ticket.Id}");
            return ticket;
        }

        private byte[] GenerateQrCode()
        {
            string qrCodeData = Guid.NewGuid().ToString(); // Use a unique identifier or any relevant data
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeDataObject = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCode(qrCodeDataObject))
                {
                    using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            qrCodeImage.Save(memoryStream, ImageFormat.Png);
                            return memoryStream.ToArray();
                        }
                    }
                }
            }
        }

    }
}
