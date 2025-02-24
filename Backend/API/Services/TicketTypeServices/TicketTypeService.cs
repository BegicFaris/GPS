using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.TicketTypeServices
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly ApplicationDbContext _context;

        public TicketTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketType>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.TicketTypes
                .ToListAsync(cancellationToken);
        }

        public async Task<TicketType?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.TicketTypes
               .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<TicketType> CreateAsync(TicketType ticketType, CancellationToken cancellationToken)
        {
            _context.TicketTypes.Add(ticketType);
            await _context.SaveChangesAsync(cancellationToken);
            return ticketType;
        }

        public async Task<TicketType> UpdateAsync(int id, TicketType ticketType, CancellationToken cancellationToken)
        {
            var existing = await _context.TicketTypes.SingleOrDefaultAsync(t => t.Id == id,cancellationToken);
            if (existing == null) throw new Exception("Ticket type with this id not found");

            existing.Name = ticketType.Name;
            existing.TenantId = ticketType.TenantId;

            await _context.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await _context.TicketTypes.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (existing == null) return false;

            _context.TicketTypes.Remove(existing);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
