using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> GetAllShiftsAsync(CancellationToken cancellationToken, bool includeDeleted=false);
        Task<Shift?> GetShiftByIdAsync(int id, CancellationToken cancellationToken);
        Task<Shift> CreateShiftAsync(Shift shift, CancellationToken cancellationToken);
        Task<Shift> UpdateShiftAsync(Shift shift, CancellationToken cancellationToken);
        Task<bool> DeleteShiftAsync(int id, CancellationToken cancellationToken);
    }
}
