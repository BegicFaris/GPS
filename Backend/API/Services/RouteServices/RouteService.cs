using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos.RouteDtos;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.RouteServices
{
    public class RouteService : IRouteService
    {
        private readonly ApplicationDbContext _context;

        public RouteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Models.Route>> GetAllRoutesAsync(CancellationToken cancellationToken) =>
             await _context.Routes
                .Include(x => x.Line)
                .Include(x => x.Station)
                .ToListAsync(cancellationToken);

        public async Task<Data.Models.Route?> GetRouteByIdAsync(int id, CancellationToken cancellationToken) =>
             await _context.Routes
                .Include(x => x.Line)
                .Include(x => x.Station)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<IEnumerable<Data.Models.Route>> GetAllRoutesByLineIdAsync(int lineId, CancellationToken cancellationToken)
        {
            return await _context.Routes
                .Include(x => x.Line)
                .Include(x => x.Station)
                .Where(x => x.LineId == lineId)
                .OrderBy(x => x.Order)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetStationCountByLineIdAsync(int lineId, CancellationToken cancellationToken)
        {

            return await _context.Routes
                .Include(x => x.Line)
                .Where(r => r.Line!=null && r.Line.Id == lineId && r.Station != null )
                .CountAsync(cancellationToken);
        }

        public async Task<bool> DeleteAllRoutesByLineIdAsync(int lineId, CancellationToken cancellationToken)
        {
            var routes = await _context.Routes
                .Where(r => r.LineId == lineId)
                .ToListAsync(cancellationToken);

            if (routes == null || routes.Count == 0)
                return false;

            _context.Routes.RemoveRange(routes);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Data.Models.Route[]> CreateRouteAsync(Data.Models.Route[] routes, CancellationToken cancellationToken)
        {

            if (routes.Length == 0) throw new ArgumentException("Routes cannot be empty.");

            var lineId = routes[0].LineId;

            for (int i = 1; i < routes.Length; i++)
                if (lineId != routes[i].LineId) throw new InvalidOperationException("All routes must be from the same line!");



            var expectedOrder = Enumerable.Range(1, routes.Length).ToArray();
            var actualOrder = routes.Select(r => r.Order).OrderBy(o => o).ToArray();

            if (!expectedOrder.SequenceEqual(actualOrder))
                throw new InvalidOperationException("Invalid route order. RouteOrder must be consecutive and start from 1.");

            _context.Routes.AddRange(routes);
            await _context.SaveChangesAsync(cancellationToken);
            return routes;
        }

        public async Task<Data.Models.Route> UpdateRouteAsync(Data.Models.Route route, CancellationToken cancellationToken)
        {
            _context.Routes.Update(route);
            await _context.SaveChangesAsync(cancellationToken);
            return route;
        }

        public async Task<bool> DeleteRouteAsync(int id, CancellationToken cancellationToken)
        {
            var route = await _context.Routes.SingleOrDefaultAsync(r => r.Id == id,cancellationToken);
            if (route == null) return false;
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
