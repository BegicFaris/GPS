using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.ShiftDetailServices
{
    public class ShiftDetailService(ApplicationDbContext context) : IShiftDetailService
    {
        public async Task<IEnumerable<ShiftDetail>> GetAllShiftDetailsAsync() =>
     await context.ShiftDetails.Include(x => x.Shift).Include(x => x.Line).ToListAsync();

        public async Task<IEnumerable<ShiftDetail>> GetShiftDetailsByShiftIdAsync(int shiftId) =>
           await context.ShiftDetails.Include(x => x.Shift).Include(x => x.Line).Where(x => x.ShiftId == shiftId).ToListAsync();

        public async Task<ShiftDetail> GetShiftDetailByIdAsync(int id) =>
          await context.ShiftDetails.Include(x => x.Shift).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> DeleteShiftDetail(int id)
        {
            var shiftDetail = await context.ShiftDetails.FindAsync(id);
            if (shiftDetail == null) return false;
            context.ShiftDetails.Remove(shiftDetail);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShiftDetailsByShiftId(int shiftId)
        {
            var shiftDetails = await context.ShiftDetails
                .Where(sd => sd.ShiftId == shiftId)
                .ToListAsync();
            if (shiftDetails == null || !shiftDetails.Any())
                return false;
            context.ShiftDetails.RemoveRange(shiftDetails);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<ShiftDetail> CreateShiftDetailAsync(ShiftDetail shiftDetail)
        {
            context.ShiftDetails.Add(shiftDetail);
            await context.SaveChangesAsync();
            return shiftDetail;
        }
    }
}
