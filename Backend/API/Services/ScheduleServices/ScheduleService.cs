using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.ScheduleServices
{
    public class ScheduleService(ApplicationDbContext _context) : IScheduleService
    {
        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync(CancellationToken cancellationToken)
        {
            return await _context.Schedules
                .Include(x => x.Line)
                .OrderBy(x => x.DepartureTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<Schedule?> GetScheduleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Schedules
                .Include(x => x.Line)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Schedule> CreateScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync(cancellationToken);
            return schedule;
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync(cancellationToken);
            return schedule;
        }

        public async Task<bool> DeleteScheduleAsync(int id, CancellationToken cancellationToken)
        {
            var schedule = await _context.Schedules.FindAsync(new object[] { id }, cancellationToken);
            if (schedule == null) return false;
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByLineIdAsync(int lineId, CancellationToken cancellationToken)
        {
            var line = await _context.Lines.FirstOrDefaultAsync(x => x.Id == lineId, cancellationToken);
            if (line == null)
                throw new Exception("No line found");
            return await _context.Schedules
                .Where(x => x.LineId == lineId)
                .OrderBy(x => x.DepartureTime)
                .ToListAsync(cancellationToken);
        }
    }
}
