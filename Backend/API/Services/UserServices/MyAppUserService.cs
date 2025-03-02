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

        public async Task<IEnumerable<MyAppUser>> GetAllUsersAsync(CancellationToken cancellationToken) =>
            await _context.MyAppUsers.ToListAsync(cancellationToken);

        public async Task<MyAppUser?> GetUserByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.MyAppUsers.SingleOrDefaultAsync(c => c.Id == id,cancellationToken);

        public async Task<MyAppUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (user == null)
            {
                return null; // or throw exception if user not found
            }

            if (user is Manager)
            {
                return await _context.Managers
                    .FirstOrDefaultAsync(m => m.Email == email, cancellationToken);
            }
            else if (user is Passenger)
            {
                return await _context.Passengers
                    .Include(d => d.Discount)
                    .FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
            }
            else if (user is Driver)
            {
                return await _context.Drivers
                    .FirstOrDefaultAsync(d => d.Email == email, cancellationToken);
            }

            return user;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken) {
            return await _context.MyAppUsers.AnyAsync(m => m.Email == email && !m.IsDeleted, cancellationToken);
        }

        public async Task<MyAppUser> CreateUserAsync(MyAppUser user, CancellationToken cancellationToken)
        {
            _context.MyAppUsers.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<MyAppUser> UpdateUserAsync(MyAppUser user, CancellationToken cancellationToken)
        {
            _context.MyAppUsers.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _context.MyAppUsers.FindAsync(new object[] { id }, cancellationToken);
            if (user == null) return false;
            _context.MyAppUsers.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
