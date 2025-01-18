using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.FavoriteLineService
{
    public class FavoriteLineService(ApplicationDbContext context) : IFavoriteLineService
    {
        public async Task<IEnumerable<FavoriteLine>> GetAllFavoriteLinesAsync() =>
            await context.FavoriteLines.Include(x => x.User).Include(x => x.Line).ToListAsync();
        public async Task<FavoriteLine> GetFavoriteLineByIdAsync(int id) =>
          await context.FavoriteLines.Include(x => x.User).Include(x => x.Line).SingleOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<FavoriteLine>> GetFavoriteLineByUserIdAsync(int userId)
        {
            var favLines = await context.FavoriteLines.Include(x=>x.Line).Where(x=>x.UserId==userId).ToListAsync(); // Fetch all lines from DB first
            return favLines.OrderBy(favLine => ExtractLeadingNumber(favLine.Line.Name))
                        .ThenBy(favLine => favLine.Line.Name); // Secondary alphabetical sorting
        }
        private int? ExtractLeadingNumber(string name)
        {
            if (string.IsNullOrEmpty(name)) return null; // Handle empty names
            var match = System.Text.RegularExpressions.Regex.Match(name, @"^\d+"); // Extract leading number
            return match.Success ? int.Parse(match.Value) : (int?)null; // Return null if no leading number
        }

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
