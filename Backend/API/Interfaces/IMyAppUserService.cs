using GPS.API.Data.Models;
namespace GPS.API.Interfaces
{
    public interface IMyAppUserService
    {
        Task<IEnumerable<MyAppUser>> GetAllUsersAsync();
        Task<MyAppUser> GetUserByIdAsync(int id);
        Task<MyAppUser> CreateUserAsync(MyAppUser user);
        Task<MyAppUser> UpdateUserAsync(MyAppUser user);
        Task<bool> DeleteUserAsync(int id);
    }
}
