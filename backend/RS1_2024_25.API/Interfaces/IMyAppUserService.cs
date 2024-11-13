using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Interfaces
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
