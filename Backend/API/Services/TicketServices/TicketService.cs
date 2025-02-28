using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using static QRCoder.PayloadGenerator;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using iText.Forms.Fields.Merging;

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

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var query = _context.Tickets.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();

                var currentTenantId = _context.CurrentTenantID;
                if (string.IsNullOrEmpty(currentTenantId))
                {
                    return new List<Ticket>(); 
                }

                query = query.Where(x => x.TenantId == currentTenantId);
            }

            query = query.Include(x => x.TicketInfo)
                         .Include(x => x.User);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<object> GetTicketsOverTimeAsync(CancellationToken cancellationToken)
        {
            var tickets = await _context.Tickets.ToListAsync(cancellationToken);

            var groupedData = tickets
                .GroupBy(t => t.CreatedDate.ToString("MMMM", CultureInfo.InvariantCulture))
                .Select(group => new
                {
                    Month = group.Key,
                    Count = group.Count()
                })
                .OrderBy(data => DateTime.ParseExact(data.Month, "MMMM", CultureInfo.InvariantCulture))
                .ToList();

            return groupedData;
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .Include(x => x.TicketInfo)
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Ticket>> GetAllTicketsForUserEmail(string email, CancellationToken cancellationToken, bool includeDeleted)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }
            var query = _context.Tickets.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();

                var currentTenantId = _context.CurrentTenantID;
                if (string.IsNullOrEmpty(currentTenantId))
                {
                    return new List<Ticket>();
                }

                query = query.Where(x => x.TenantId == currentTenantId);
            }

            query = query
                .Include(x => x.TicketInfo)
                .Include(x => x.TicketInfo != null ? x.TicketInfo.Zone : null)
                .Include(x => x.TicketInfo != null ? x.TicketInfo.Zone : null)
                .Include(x => x.User)
                .Where(t => t.User != null && t.User.Email == email);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);
            return ticket;
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);
            return ticket;
        }

        public async Task<bool> DeleteTicketAsync(int id, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Ticket> CreateTicketOnBuying(int ticketInfoId, string userEmail, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating ticket for user: {userEmail}, TicketInfoId: {ticketInfoId}");
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken);
            if (user == null)
            {
                _logger.LogError($"User not found: {userEmail}");
                throw new Exception("User not found");
            }

            var ticketInfo = await _context.TicketInfos.Include(ti => ti.TicketType)
                .FirstOrDefaultAsync(ti => ti.Id == ticketInfoId, cancellationToken);
            if (ticketInfo == null)
            {
                _logger.LogError($"Ticket info not found: {ticketInfoId}");
                throw new Exception("Ticket info not found");
            }
            if (ticketInfo.TicketType == null)
            {
                _logger.LogError($"Ticket type for ticket not found: {ticketInfoId}");
                throw new Exception("Ticket type for ticket not found");
            }

            int expirationDays = ticketInfo.TicketType.Name == "Monthly" ? 30 : 1;

            var ticket = new Ticket
            {
                UserId = user.Id,
                TicketInfoId = ticketInfoId,
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(expirationDays),
                QrCode = GenerateQrCode()
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);

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
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            try
                            {


                                #pragma warning disable CA1416 // Validate platform compatibility
                                qrCodeImage.Save(memoryStream, ImageFormat.Png);
                                #pragma warning restore CA1416 // Validate platform compatibility


                            }
                            catch (Exception ex)
                            {
                                // Log the error and throw an exception if saving fails
                                _logger.LogError(ex, "Failed to save QR code image to memory stream.");
                                throw new Exception("Failed to save QR code image.", ex);
                            }

                            return memoryStream.ToArray();
                        }
                    }
                }
            }
        }


        public async Task<object> GetUserTicketsPaginatedAsync(string email, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var tickets = await _context.Tickets
                .Include(x => x.TicketInfo)
                .Include(x => x.User)
                .Include(x => x.TicketInfo != null ? x.TicketInfo.TicketType     : null)
                .Include(x => x.TicketInfo != null ? x.TicketInfo.Zone : null)
                .Where(t => t.User!=null && t.User.Email == email)
                .OrderByDescending(t => t.CreatedDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

            var totalTickets = await _context.Tickets
                .Include(x => x.User)
                .Where(x => x.User!=null && x.User.Email == email)
                .CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);
            return new
            {
                TotalTickets = totalTickets,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Tickets = tickets
            };
        }
    }
}
