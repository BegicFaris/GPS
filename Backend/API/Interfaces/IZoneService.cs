using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IZoneService
    {
        Task<IEnumerable<Zone>> GetAllZonesAsync(CancellationToken cancellationToken);
        Task<Zone?> GetZoneByIdAsync(int id, CancellationToken cancellationToken);
    }
}
