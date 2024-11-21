using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetAllStationsAsync();
        Task<Station> GetStationByIdAsync(int id);
        Task<Station> CreateStationAsync(Station station);
        Task<Station> UpdateStationAsync(Station station);
        Task<bool> DeleteStationAsync(int id);
    }
}
