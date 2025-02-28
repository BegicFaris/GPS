using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.StationServices
{
    public class StationService : IStationService
    {
        private readonly ApplicationDbContext _context;

        public StationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Station>> GetAllStationsAsync(CancellationToken cancellationToken)
        {
            var query = _context.Stations.AsQueryable();
            return await query.Include(x => x.Zone).ToListAsync(cancellationToken);
        }
        public async Task<Station?> GetStationByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Stations.Include(x => x.Zone).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Station> CreateStationAsync(Station station, CancellationToken cancellationToken)
        {
            _context.Stations.Add(station);
            await _context.SaveChangesAsync(cancellationToken);
            return station;
        }

        public async Task<Station> UpdateStationAsync(Station station, CancellationToken cancellationToken)
        {
            _context.Stations.Update(station);
            await _context.SaveChangesAsync(cancellationToken);
            return station;
        }

        public async Task<bool> DeleteStationAsync(int id, CancellationToken cancellationToken)
        {
            var station = await _context.Stations.FindAsync(new object[] { id }, cancellationToken);
            if (station == null) throw new KeyNotFoundException("StationId not found!");

            bool isInUse = await _context.Routes.AnyAsync(r => r.StationId == id, cancellationToken);
            if (isInUse) throw new InvalidOperationException("Cannot delete station because it is associated with a route.");

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
