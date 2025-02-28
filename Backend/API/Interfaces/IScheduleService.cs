using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync(CancellationToken cancellationToken, bool includeDeleted=false);
        Task<Schedule?> GetScheduleByIdAsync(int id, CancellationToken cancellationToken);
        Task<Schedule> CreateScheduleAsync(Schedule schedule, CancellationToken cancellationToken);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken);
        Task<bool> DeleteScheduleAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Schedule>> GetAllSchedulesByLineIdAsync(int lineId, CancellationToken cancellationToken, bool includeDeleted=false);
    }
}
