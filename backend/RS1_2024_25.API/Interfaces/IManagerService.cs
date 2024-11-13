using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
{
    public interface IManagerService
    {
        Task<IEnumerable<Manager>> GetAllManagersAsync();
        Task<Manager> GetManagerByIdAsync(int id);
        Task<Manager> CreateManagerAsync(Manager manager);
        Task<Manager> UpdateManagerAsync(Manager manager);
        Task<bool> DeleteManagerAsync(int id);
    }
}
