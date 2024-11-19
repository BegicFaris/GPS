using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.RouteServices
{
    public class RouteService : IRouteService
    {
        private readonly ApplicationDbContext _context;
        public RouteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Data.Models.Route>> GetAllRoutesAsync() =>
            await _context.Routes.ToListAsync();
        public async Task<Data.Models.Route> GetRouteByIdAsync(int id) =>
          await _context.Routes.FindAsync(id);
        public async Task<Data.Models.Route> CreateRouteAsync(Data.Models.Route route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public async Task<Data.Models.Route> UpdateRouteAsync(Data.Models.Route route)
        {
            _context.Routes.Update(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public async Task<bool> DeleteRouteAsync(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null) return false;
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
