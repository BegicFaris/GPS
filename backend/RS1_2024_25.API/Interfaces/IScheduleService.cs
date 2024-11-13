using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule?> GetScheduleByIdAsync(int id);
        Task<Schedule> CreateScheduleAsync(Schedule schedule);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule);
        Task<bool> DeleteScheduleAsync(int id);
    }
}
