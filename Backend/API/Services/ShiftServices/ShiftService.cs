using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.ShiftServices
{
    public class ShiftService : IShiftService
    {
        private readonly ApplicationDbContext _context;
        public ShiftService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shift>> GetAllShiftsAsync() =>
            await _context.Shifts.Include(x => x.Bus).Include(x => x.Driver).ToListAsync();
        public async Task<Shift> GetShiftByIdAsync(int id) =>
          await _context.Shifts.Include(x => x.Bus).Include(x => x.Driver).SingleOrDefaultAsync(x => x.Id == id);
        public async Task<Shift> CreateShiftAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return shift;
        }


        public async Task<Shift> UpdateShiftAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<bool> DeleteShiftAsync(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return false;
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
