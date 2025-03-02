using GPS.API.Data.Models;
using GPS.API.Dtos.ShiftDtos;

namespace GPS.API.Interfaces
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> GetAllShiftsAsync(CancellationToken cancellationToken, bool includeDeleted=false);
        Task<Shift?> GetShiftByIdAsync(int id, CancellationToken cancellationToken);
        Task<Shift> CreateShiftAsync(Shift shift, CancellationToken cancellationToken);
        Task<Shift> UpdateShiftAsync(Shift shift, CancellationToken cancellationToken);
        Task<bool> DeleteShiftAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ShiftDto>> GetDriverShiftsAsync(CancellationToken cancellationToken, int driverId, DateTime? fromDate = null, DateTime? toDate = null);
        Task<ShiftDetailsDto> GetShiftDetailsAsync(int shiftId, CancellationToken cancellationToken);
        Task<byte[]> GenerateShiftPdfAsync(int shiftId, CancellationToken cancellationToken);
        Task<IEnumerable<ShiftDto>> GetCurrentShiftsAsync(int driverId, CancellationToken cancellationToken);
        Task<IEnumerable<ShiftDto>> GetUpcomingShiftsAsync(int driverId, CancellationToken cancellationToken);
        Task<IEnumerable<ShiftDto>> GetEndedShiftsAsync(int driverId, CancellationToken cancellationToken);
    }
}
