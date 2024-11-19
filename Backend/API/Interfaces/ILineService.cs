
using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ILineService
    {
        Task<IEnumerable<Line>> GetAllLinesAsync();
        Task<Line?> GetLineByIdAsync(int id);
        Task<Line> CreateLineAsync(Line line);
        Task<Line> UpdateLineAsync(Line line);
        Task<bool> DeleteLineAsync(int id);
    }
}
