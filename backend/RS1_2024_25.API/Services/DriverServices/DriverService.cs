using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Services.DriverServices
{
    public class DriverService: IDriverService
    {
        private readonly ApplicationDbContext _context;

        public DriverService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync() =>
            await _context.Drivers.ToListAsync();

        public async Task<Driver> GetDriverByIdAsync(int id) =>
            await _context.Drivers.FindAsync(id);

        public async Task<Driver> CreateDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return false;
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
