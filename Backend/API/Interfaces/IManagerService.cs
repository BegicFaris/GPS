using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IManagerService
    {
        Task<IEnumerable<Manager>> GetAllManagersAsync(CancellationToken cancellationToken );
        Task<Manager?> GetManagerByIdAsync(int id, CancellationToken cancellationToken );
        Task<Manager> CreateManagerAsync(Manager manager, CancellationToken cancellationToken );
        Task<Manager> UpdateManagerAsync(Manager manager, CancellationToken cancellationToken );
        Task<bool> DeleteManagerAsync(int id, CancellationToken cancellationToken );
    }
}
