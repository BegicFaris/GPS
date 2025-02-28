using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using System.Threading; // Needed for CancellationToken

namespace GPS.API.Services.DriverServices
{
    public class DriverService : IDriverService
    {
        private readonly ApplicationDbContext _context;

        public DriverService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync(CancellationToken cancellationToken) =>
            await _context.Drivers.ToListAsync(cancellationToken);

        public async Task<Driver?> GetDriverByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Drivers.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Driver> CreateDriverAsync(Driver driver, CancellationToken cancellationToken)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync(cancellationToken);
            return driver;
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver, CancellationToken cancellationToken)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync(cancellationToken);
            return driver;
        }

        public async Task<bool> DeleteDriverAsync(int id, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.SingleOrDefaultAsync(x=>x.Id==id,cancellationToken); 
            if (driver == null) throw new KeyNotFoundException("Driver not found."); 


            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
