using GPS.API.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Interfaces
{
    public interface IShiftDetailService
    {
        Task<IEnumerable<ShiftDetail>> GetAllShiftDetailsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ShiftDetail>> GetShiftDetailsByShiftIdAsync(int shiftId, CancellationToken cancellationToken);
        Task<ShiftDetail?> GetShiftDetailByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> DeleteShiftDetail(int id, CancellationToken cancellationToken);
        Task<bool> DeleteShiftDetailsByShiftId(int shiftId, CancellationToken cancellationToken);
        Task<ShiftDetail> CreateShiftDetailAsync(ShiftDetail shiftDetail, CancellationToken cancellationToken);
        Task<byte[]> GeneratePdfAsync(int shiftId, CancellationToken cancellationToken);
    }
}
