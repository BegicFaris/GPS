using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IPassengerService
    {
        Task<IEnumerable<Passenger>> GetAllPassengersAsync();
        Task<Passenger> GetPassengerByIdAsync(int id);
        Task<Passenger> CreatePassengerAsync(Passenger passenger);
        Task<Passenger> UpdatePassengerAsync(Passenger passenger);
        Task<bool> DeletePassengerAsync(int id);
    }
}
