using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IBusService
    {
        Task<IEnumerable<Bus>> GetAllBusesAsync(CancellationToken cancellationToken);
        Task<Bus?> GetBusByIdAsync(int id, CancellationToken cancellationToken);
        Task<Bus> CreateBusAsync(Bus bus, CancellationToken cancellationToken);
        Task<Bus> UpdateBusAsync(Bus bus, CancellationToken cancellationToken);
        Task<bool> DeleteBusAsync(int id, CancellationToken cancellationToken);
    }
}
