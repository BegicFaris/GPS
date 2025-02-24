
using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDriversAsync(CancellationToken cancellationToken);
        Task<Driver?> GetDriverByIdAsync(int id, CancellationToken cancellationToken);
        Task<Driver> CreateDriverAsync(Driver driver, CancellationToken cancellationToken);
        Task<Driver> UpdateDriverAsync(Driver driver, CancellationToken cancellationToken);
        Task<bool> DeleteDriverAsync(int id, CancellationToken cancellationToken);
    }
}
