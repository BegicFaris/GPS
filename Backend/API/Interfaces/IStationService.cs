using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetAllStationsAsync(CancellationToken cancellationToken);
        Task<Station?> GetStationByIdAsync(int id, CancellationToken cancellationToken);
        Task<Station> CreateStationAsync(Station station, CancellationToken cancellationToken);
        Task<Station> UpdateStationAsync(Station station, CancellationToken cancellationToken);
        Task<bool> DeleteStationAsync(int id, CancellationToken cancellationToken);
    }
}
