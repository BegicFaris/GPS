using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IPassengerService
    {
        Task<IEnumerable<Passenger>> GetAllPassengersAsync(CancellationToken cancellationToken, bool includeDeleted = false);
        Task<Passenger?> GetPassengerByIdAsync(int id, CancellationToken cancellationToken);
        Task<Passenger> CreatePassengerAsync(Passenger passenger, CancellationToken cancellationToken);
        Task<Passenger> UpdatePassengerAsync(Passenger passenger, CancellationToken cancellationToken);
        Task<bool> DeletePassengerAsync(int id, CancellationToken cancellationToken);
    }
}
