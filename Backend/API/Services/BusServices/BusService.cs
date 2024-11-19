using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.BusServices
{
    public class BusService : IBusService
    {
        private readonly ApplicationDbContext _context;
        public BusService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Bus>> GetAllBusesAsync() =>
            await _context.Buses.ToListAsync();
        public async Task<Bus> GetBusByIdAsync(int id) =>
          await _context.Buses.FindAsync(id);
        public async Task<Bus> CreateBusAsync(Bus bus)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
            return bus;
        }

        public async Task<Bus> UpdateBusAsync(Bus bus)
        {
            _context.Buses.Update(bus);
            await _context.SaveChangesAsync();
            return bus;
        }

        public async Task<bool> DeleteBusAsync(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null) return false;
            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
