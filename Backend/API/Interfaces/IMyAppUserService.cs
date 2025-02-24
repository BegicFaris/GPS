using GPS.API.Data.Models;
using GPS.API.Dtos.TicketDtos;
using GPS.API.Dtos.UserDtos;
namespace GPS.API.Interfaces
{
    public interface IMyAppUserService
    {
        Task<IEnumerable<MyAppUser>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<MyAppUser?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
        Task<MyAppUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<MyAppUser> CreateUserAsync(MyAppUser user, CancellationToken cancellationToken);
        Task<MyAppUser> UpdateUserAsync(MyAppUser user, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken);
    }
}
