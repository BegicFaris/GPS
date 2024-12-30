using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.TicketTypeServices
{
    public class TicketTypeService: ITicketTypeService
    {
        private readonly ApplicationDbContext _context;

        public TicketTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketType>> GetAllAsync()
        {
            return await _context.Set<TicketType>().ToListAsync();
        }

        public async Task<TicketType?> GetByIdAsync(int id)
        {
            return await _context.Set<TicketType>().FindAsync(id);
        }

        public async Task<TicketType> CreateAsync(TicketType ticketType)
        {
            _context.Set<TicketType>().Add(ticketType);
            await _context.SaveChangesAsync();
            return ticketType;
        }

        public async Task<TicketType?> UpdateAsync(int id, TicketType ticketType)
        {
            var existing = await _context.Set<TicketType>().FindAsync(id);
            if (existing == null) return null;

            existing.Name = ticketType.Name;
            existing.TenantId = ticketType.TenantId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Set<TicketType>().FindAsync(id);
            if (existing == null) return false;

            _context.Set<TicketType>().Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
