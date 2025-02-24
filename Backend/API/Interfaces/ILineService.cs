
using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ILineService
    {
        Task<IEnumerable<Line>> GetAllLinesAsync(string? lineName, string? stationName, CancellationToken cancellationToken);
        Task<IEnumerable<Line>> GetAllLinesByStationIdAsync(int stationId, CancellationToken cancellationToken);
        Task<Line?> GetLineByIdAsync(int id, CancellationToken cancellationToken );
        Task<Line> CreateLineAsync(Line line, CancellationToken cancellationToken);
        Task<Line> UpdateLineAsync(Line line, CancellationToken cancellationToken);
        Task<bool> DeleteLineAsync(int id, CancellationToken cancellationToken);
    }   
}
