using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Services.PassengerServices
{
    public class PassengerService: IPassengerService
    {
        private readonly ApplicationDbContext _context;

        public PassengerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync() =>
            await _context.Passengers.ToListAsync();

        public async Task<Passenger> GetPassengerByIdAsync(int id) =>
            await _context.Passengers.FindAsync(id);

        public async Task<Passenger> CreatePassengerAsync(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<Passenger> UpdatePassengerAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<bool> DeletePassengerAsync(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null) return false;
            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
