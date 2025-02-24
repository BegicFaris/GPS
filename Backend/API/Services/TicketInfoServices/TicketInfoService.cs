using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.TicketInfoServices
{
    public class TicketInfoService : ITicketInfoService
    {
        private readonly ApplicationDbContext _context;

        public TicketInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketInfo>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.TicketInfos
                .Include(t => t.Zone)
                .Include(t => t.TicketType)
                .ToListAsync(cancellationToken);
        }

        public async Task<TicketInfo?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.TicketInfos
                .Include(t => t.Zone)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<TicketInfo> CreateAsync(TicketInfo ticketInfo, CancellationToken cancellationToken)
        {
            _context.TicketInfos.Add(ticketInfo);
            await _context.SaveChangesAsync(cancellationToken);
            return ticketInfo;
        }

        public async Task<TicketInfo> UpdateAsync(int id, TicketInfo ticketInfo, CancellationToken cancellationToken)
        {
            var existing = await _context.TicketInfos.SingleOrDefaultAsync(x=>x.Id == id,cancellationToken) ?? throw new Exception("Ticket info with this idd not found");
            existing.Price = ticketInfo.Price;
            existing.ZoneId = ticketInfo.ZoneId;
            existing.TicketTypeId = ticketInfo.TicketTypeId;
            existing.TenantId = ticketInfo.TenantId;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var ticketInfo = await _context.TicketInfos.FindAsync(id);
            if (ticketInfo == null) return false;

            _context.TicketInfos.Remove(ticketInfo);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<TicketInfo>> GetByTicketTypeIdAsync(int ticketTypeId, CancellationToken cancellationToken)
        {
            return await _context.TicketInfos
                .Include(t => t.Zone)
                .Include(t => t.TicketType)
                .Where(t => t.TicketTypeId == ticketTypeId)
                .ToListAsync(cancellationToken);
        }
    }
}
