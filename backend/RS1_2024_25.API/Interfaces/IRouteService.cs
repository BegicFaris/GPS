using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<Data.Models.Route>> GetAllRoutesAsync();
        Task<Data.Models.Route> GetRouteByIdAsync(int id);
        Task<Data.Models.Route> CreateRouteAsync(Data.Models.Route route);
        Task<Data.Models.Route> UpdateRouteAsync(Data.Models.Route route);
        Task<bool> DeleteRouteAsync(int id);
    }
}
