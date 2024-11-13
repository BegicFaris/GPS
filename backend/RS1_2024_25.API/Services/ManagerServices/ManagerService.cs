using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Services.ManagerServices
{
    public class ManagerService: IManagerService
    {
        private readonly ApplicationDbContext _context;

        public ManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manager>> GetAllManagersAsync() =>
            await _context.Managers.ToListAsync();

        public async Task<Manager> GetManagerByIdAsync(int id) =>
            await _context.Managers.FindAsync(id);

        public async Task<Manager> CreateManagerAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();
            return manager;
        }

        public async Task<Manager> UpdateManagerAsync(Manager manager)
        {
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync();
            return manager;
        }

        public async Task<bool> DeleteManagerAsync(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null) return false;
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
