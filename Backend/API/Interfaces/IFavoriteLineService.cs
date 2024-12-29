using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IFavoriteLineService
    {
        Task<IEnumerable<FavoriteLine>> GetAllFavoriteLinesAsync();
        Task<IEnumerable<FavoriteLine>> GetFavoriteLineByUserIdAsync(int userId);
        Task<FavoriteLine> GetFavoriteLineByIdAsync(int id);
        Task<FavoriteLine> CreateFavoriteLineAsync(FavoriteLine favoriteLine);
        Task<bool> DeleteFavoriteLineAsync(int id);
    }
}

