using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IFavoriteLineService
    {
        Task<IEnumerable<FavoriteLine>> GetAllFavoriteLinesAsync(CancellationToken cancellationToken, bool includeDeleted = false);
        Task<FavoriteLine?> GetFavoriteLineByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<FavoriteLine>> GetFavoriteLineByUserIdAsync(int userId, CancellationToken cancellationToken, bool includeDeleted=false);
        Task<FavoriteLine> CreateFavoriteLineAsync(FavoriteLine favoriteLine, CancellationToken cancellationToken);
        Task<bool> DeleteFavoriteLineAsync(int id, CancellationToken cancellationToken);
    }
}

