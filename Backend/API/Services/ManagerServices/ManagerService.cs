using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.ManagerServices
{
    public class ManagerService : IManagerService
    {
        private readonly ApplicationDbContext _context;

        public ManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manager>> GetAllManagersAsync(CancellationToken cancellationToken) =>
            await _context.Managers.ToListAsync(cancellationToken);

        public async Task<Manager?> GetManagerByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Managers.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Manager> CreateManagerAsync(Manager manager, CancellationToken cancellationToken)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync(cancellationToken);
            return manager;
        }

        public async Task<Manager> UpdateManagerAsync(Manager manager, CancellationToken cancellationToken)
        {
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync(cancellationToken);
            return manager;
        }

        public async Task<bool> DeleteManagerAsync(int id, CancellationToken cancellationToken)
        {
            var manager = await _context.Managers.FindAsync(new object[] { id }, cancellationToken);
            if (manager == null) return false;
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
