using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IZoneService
    {
        Task<IEnumerable<Zone>> GetAllZonesAsync();
        Task<Zone> GetZoneByIdAsync(int id);
    }
}
