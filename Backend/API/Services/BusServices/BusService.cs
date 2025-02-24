using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading; // Needed for CancellationToken

namespace GPS.API.Services.BusServices
{
    public class BusService : IBusService
    {
        private readonly ApplicationDbContext _context;

        public BusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bus>> GetAllBusesAsync(CancellationToken cancellationToken) =>
            await _context.Buses.ToListAsync(cancellationToken);

        public async Task<Bus?> GetBusByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Buses.FindAsync(id,cancellationToken);  

        public async Task<Bus> CreateBusAsync(Bus bus, CancellationToken cancellationToken)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync(cancellationToken);
            return bus;
        }

        public async Task<Bus> UpdateBusAsync(Bus bus, CancellationToken cancellationToken)
        {
            _context.Buses.Update(bus);
            await _context.SaveChangesAsync(cancellationToken);
            return bus;
        }

        public async Task<bool> DeleteBusAsync(int id, CancellationToken cancellationToken)
        {
            var bus = await _context.Buses.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);  // No change to FindAsync
            if (bus == null) throw new KeyNotFoundException("Driver not found.");

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
