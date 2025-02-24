using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITenantService
    {
        Task<IEnumerable<Tenant>> GetAllTenantsAsync(CancellationToken cancellationToken);
    }
}
