using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.ShiftServices
{
    public class ShiftService : IShiftService
    {
        private readonly ApplicationDbContext _context;

        public ShiftService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync(CancellationToken cancellationToken) =>
            await _context.Shifts.Include(x => x.Bus).Include(x => x.Driver).ToListAsync(cancellationToken);

        public async Task<Shift?> GetShiftByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Shifts.Include(x => x.Bus).Include(x => x.Driver).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Shift> CreateShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return shift;
        }

        public async Task<Shift> UpdateShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return shift;
        }

        public async Task<bool> DeleteShiftAsync(int id, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.FindAsync(new object[] { id }, cancellationToken);
            if (shift == null) return false;
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
