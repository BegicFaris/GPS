using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.ScheduleServices
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;
        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync() =>
            await _context.Schedules.Include(x => x.Line).OrderBy(x=>x.DepartureTime).ToListAsync();

        public async Task<Schedule?> GetScheduleByIdAsync(int id) =>
          await _context.Schedules.Include(x => x.Line).SingleOrDefaultAsync(x => x.Id == id);
        public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return false;
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByLineIdAsync(int lineId)
        {
            var line = await  _context.Lines.FirstOrDefaultAsync(x=>x.Id == lineId);
            if (line == null)
                throw new Exception("No line found");
            return await _context.Schedules.Where(x => x.LineId == lineId).OrderBy(x=>x.DepartureTime).ToListAsync();
        }
    }
}
