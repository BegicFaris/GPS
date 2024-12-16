using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.StationServices
{
    public class StationService:IStationService
    {
        private readonly ApplicationDbContext _context;
        public StationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Station>> GetAllStationsAsync() =>
            await _context.Stations.Include(x => x.Zone).ToListAsync();
        public async Task<Station> GetStationByIdAsync(int id) =>
          await _context.Stations.Include(x => x.Zone).SingleOrDefaultAsync(x => x.Id == id);
        public async Task<Station> CreateStationAsync(Station station)
        {
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<Station> UpdateStationAsync(Station station)
        {
            _context.Stations.Update(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<bool> DeleteStationAsync(int id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null) return false;
            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
