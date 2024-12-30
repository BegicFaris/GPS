using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.FavoriteLineService
{
    public class FavoriteLineService(ApplicationDbContext context) : IFavoriteLineService
    {
        public async Task<IEnumerable<FavoriteLine>> GetAllFavoriteLinesAsync() =>
            await context.FavoriteLines.Include(x => x.User).Include(x => x.Line).ToListAsync();
        public async Task<FavoriteLine> GetFavoriteLineByIdAsync(int id) =>
          await context.FavoriteLines.Include(x => x.User).Include(x => x.Line).SingleOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<FavoriteLine>> GetFavoriteLineByUserIdAsync(int userId) =>
          await context.FavoriteLines.Include(x => x.User).Include(x => x.Line).Where(x=>x.UserId==userId).ToListAsync();
        public async Task<FavoriteLine> CreateFavoriteLineAsync(FavoriteLine favoriteLine)
        {
            context.FavoriteLines.Add(favoriteLine);
            await context.SaveChangesAsync();
            return favoriteLine;
        }
        public async Task<bool> DeleteFavoriteLineAsync(int id)
        {
            var favoriteLine = await context.FavoriteLines.FindAsync(id);
            if (favoriteLine == null) return false;
            context.FavoriteLines.Remove(favoriteLine);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
