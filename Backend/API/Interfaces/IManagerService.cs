using GPS.API.Data.Models;

namespace GPS.API.Interfaces
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
