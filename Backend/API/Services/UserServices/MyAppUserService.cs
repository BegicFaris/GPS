using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Dtos.UserDtos;
using GPS.API.Dtos.TicketDtos;

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
        public async Task<MyAppUser> GetUserByEmailAsync(string email)
        {
            var user = await _context.MyAppUsers
                                 .FirstOrDefaultAsync(u => u.Email == email);
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }
            if (user == null)
            {
                return null;  // or throw exception if user not found
            }

            if (user is Manager)
            {
                return await _context.Managers.FirstOrDefaultAsync(m => m.Email == email);
            }
            else if (user is Passenger)
            {
                return await _context.Passengers.Include(d=>d.Discount).FirstOrDefaultAsync(p => p.Email == email);
            }
            else if (user is Driver)
            {
                return await _context.Drivers.FirstOrDefaultAsync(d => d.Email == email);
            }
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.MyAppUsers.AnyAsync(u => u.Email == email);
        }


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
