using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDriversAsync();
        Task<Driver> GetDriverByIdAsync(int id);
        Task<Driver> CreateDriverAsync(Driver driver);
        Task<Driver> UpdateDriverAsync(Driver driver);
        Task<bool> DeleteDriverAsync(int id);
    }
}
