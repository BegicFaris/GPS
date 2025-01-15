using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<Data.Models.Route>> GetAllRoutesAsync();
        Task<Data.Models.Route> GetRouteByIdAsync(int id);
        Task<IEnumerable<Data.Models.Route>> GetAllRoutesByLineIdAsync(int lineId);
        Task<int> GetStationCountByLineIdAsync(int lineId);
        Task<bool> DeleteAllRoutesByLineIdAsync(int lineId);
        Task<Data.Models.Route> CreateRouteAsync(Data.Models.Route route);
        Task<Data.Models.Route> UpdateRouteAsync(Data.Models.Route route);
        Task<bool> DeleteRouteAsync(int id);
    }
}
