using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.TicketInfoServices
{
    public class TicketInfoService:ITicketInfoService
    {
        private readonly ApplicationDbContext _context;

        public TicketInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketInfo>> GetAllAsync()
        {
            return await _context.Set<TicketInfo>()
                .Include(t => t.Zone)
                .Include(t => t.TicketType)
                .ToListAsync();
        }

        public async Task<TicketInfo?> GetByIdAsync(int id)
        {
            return await _context.Set<TicketInfo>()
                .Include(t => t.Zone)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TicketInfo> CreateAsync(TicketInfo ticketInfo)
        {
            _context.Set<TicketInfo>().Add(ticketInfo);
            await _context.SaveChangesAsync();
            return ticketInfo;
        }

        public async Task<TicketInfo> UpdateAsync(int id, TicketInfo ticketInfo)
        {
            var existing = await _context.Set<TicketInfo>().FindAsync(id);
            if (existing == null) return null;

            existing.Price = ticketInfo.Price;
            existing.ZoneId = ticketInfo.ZoneId;
            existing.TicketTypeId = ticketInfo.TicketTypeId;
            existing.TenantId = ticketInfo.TenantId;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticketInfo = await _context.Set<TicketInfo>().FindAsync(id);
            if (ticketInfo == null) return false;

            _context.Set<TicketInfo>().Remove(ticketInfo);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TicketInfo>> GetByTicketTypeIdAsync(int ticketTypeId)
        {
            return await _context.Set<TicketInfo>()
                .Include(t => t.Zone)
                .Include(t => t.TicketType)
                .Where(t => t.TicketTypeId == ticketTypeId)
                .ToListAsync();
        }
    }
}

