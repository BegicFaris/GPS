using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
{
    public interface IBusService
    {
        Task<IEnumerable<Bus>> GetAllBusesAsync();
        Task<Bus> GetBusByIdAsync(int id);
        Task<Bus> CreateBusAsync(Bus bus);
        Task<Bus> UpdateBusAsync(Bus bus);
        Task<bool> DeleteBusAsync(int id);
    }
}
