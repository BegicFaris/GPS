using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.ZoneServices
{
    public class ZoneService : IZoneService
    {
        private readonly ApplicationDbContext _context;

        public ZoneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Zone>> GetAllZonesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Zones.ToListAsync(cancellationToken);
        }

        public async Task<Zone?> GetZoneByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Zones.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
