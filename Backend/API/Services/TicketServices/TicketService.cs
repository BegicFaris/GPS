using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync() =>
            await _context.Tickets.ToListAsync();
        public async Task<Ticket> GetTicketByIdAsync(int id) =>
          await _context.Tickets.FindAsync(id);
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

    }
}
