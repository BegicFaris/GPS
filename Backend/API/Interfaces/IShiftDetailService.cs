using GPS.API.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Interfaces
{
    public interface IShiftDetailService
    {
        Task<IEnumerable<ShiftDetail>> GetAllShiftDetailsAsync();
        Task<IEnumerable<ShiftDetail>> GetShiftDetailsByShiftIdAsync(int shiftId);
        Task<ShiftDetail> GetShiftDetailByIdAsync(int id);
        Task<bool> DeleteShiftDetail(int id);
        Task<bool> DeleteShiftDetailsByShiftId(int shiftId);
        Task<ShiftDetail> CreateShiftDetailAsync(ShiftDetail shiftDetail);
        Task<byte[]> GeneratePdfAsync(int shiftId);
    }
}
