using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
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
