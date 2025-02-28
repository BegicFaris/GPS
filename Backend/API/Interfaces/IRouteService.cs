using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<Data.Models.Route>> GetAllRoutesAsync(CancellationToken cancellationToken, bool includeDeleted = false);
        Task<Data.Models.Route?> GetRouteByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Data.Models.Route>> GetAllRoutesByLineIdAsync(int lineId, CancellationToken cancellationToken, bool includeDeleted = false);
        Task<int> GetStationCountByLineIdAsync(int lineId, CancellationToken cancellationToken);
        Task<bool> DeleteAllRoutesByLineIdAsync(int lineId, CancellationToken cancellationToken);
        Task<Data.Models.Route[]> CreateRouteAsync(Data.Models.Route[] routes, CancellationToken cancellationToken);
        Task<Data.Models.Route> UpdateRouteAsync(Data.Models.Route route, CancellationToken cancellationToken);
        Task<bool> DeleteRouteAsync(int id, CancellationToken cancellationToken);
    }
}
