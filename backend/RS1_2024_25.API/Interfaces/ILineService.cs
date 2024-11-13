using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
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
