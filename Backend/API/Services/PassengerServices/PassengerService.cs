using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.PassengerServices
{
    public class PassengerService : IPassengerService
    {
        private readonly ApplicationDbContext _context;

        public PassengerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync(CancellationToken cancellationToken)
        {
            var query = _context.Passengers.AsQueryable();
            query = query.IgnoreQueryFilters();

            var currentTenantId = _context.CurrentTenantID;

            if (string.IsNullOrEmpty(currentTenantId))
            {
                return new List<Passenger>();
            }

            query = query.Where(x => x.TenantId == currentTenantId);
            query = query.Include(x => x.DiscountID==null?null:x.Discount);

            return await query.ToListAsync(cancellationToken);


        }


        public async Task<Passenger?> GetPassengerByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Passengers.Include(x => x.Discount).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Passenger> CreatePassengerAsync(Passenger passenger, CancellationToken cancellationToken)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync(cancellationToken);
            return passenger;
        }

        public async Task<Passenger> UpdatePassengerAsync(Passenger passenger, CancellationToken cancellationToken)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync(cancellationToken);
            return passenger;
        }

        public async Task<bool> DeletePassengerAsync(int id, CancellationToken cancellationToken)
        {
            var passenger = await _context.Passengers.FindAsync(new object[] { id }, cancellationToken);
            if (passenger == null) return false;
            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
