using GPS.API.Data.Models;
using GPS.API.Dtos.TicketDtos;
using GPS.API.Dtos.UserDtos;
namespace GPS.API.Interfaces
{
    public interface IMyAppUserService
    {
        Task<IEnumerable<MyAppUser>> GetAllUsersAsync();
        Task<MyAppUser> GetUserByIdAsync(int id);
        Task<MyAppUser> GetUserByEmailAsync(string email);
        Task<MyAppUser> CreateUserAsync(MyAppUser user);
        Task<bool> EmailExistsAsync(string email);
        Task<MyAppUser> UpdateUserAsync(MyAppUser user);
        Task<bool> DeleteUserAsync(int id);
    }
}
