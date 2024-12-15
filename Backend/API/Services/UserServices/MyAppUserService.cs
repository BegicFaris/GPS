using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Services.UserServices
{
    public class MyAppUserService : IMyAppUserService
    {
        private readonly ApplicationDbContext _context;

        public MyAppUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MyAppUser>> GetAllUsersAsync() =>
            await _context.MyAppUsers.ToListAsync();

        public async Task<MyAppUser> GetUserByIdAsync(int id) =>
            await _context.MyAppUsers.FindAsync(id);

        public async Task<MyAppUser> CreateUserAsync(MyAppUser user)
        {
            _context.MyAppUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<MyAppUser> UpdateUserAsync(MyAppUser user)
        {
            _context.MyAppUsers.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.MyAppUsers.FindAsync(id);
            if (user == null) return false;
            _context.MyAppUsers.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
