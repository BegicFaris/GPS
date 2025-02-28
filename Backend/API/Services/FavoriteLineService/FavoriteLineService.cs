using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.FavoriteLineService
{
    public class FavoriteLineService : IFavoriteLineService
    {
        private readonly ApplicationDbContext context;

        public FavoriteLineService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<FavoriteLine>> GetAllFavoriteLinesAsync(CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var query = context.FavoriteLines.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();

                var currentTenantId = context.CurrentTenantID;
                if (string.IsNullOrEmpty(currentTenantId))
                {
                    return new List<FavoriteLine>();
                }

                query = query.Where(x => x.TenantId == currentTenantId);
            }

            query = query.Include(x => x.User).Include(x => x.Line);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<FavoriteLine?> GetFavoriteLineByIdAsync(int id, CancellationToken cancellationToken) =>
            await context.FavoriteLines.Include(x => x.User).Include(x => x.Line).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<IEnumerable<FavoriteLine>> GetFavoriteLineByUserIdAsync(int userId, CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var query = context.FavoriteLines.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(x => x.Line != null && !x.Line.IsDeleted);
            }

            var currentTenantId = context.CurrentTenantID;
            if (!string.IsNullOrEmpty(currentTenantId))
            {
                query = query.Where(x => x.TenantId == currentTenantId);
            }

            var favLines = await query.Include(x => x.Line)
                                       .Where(x => x.UserId == userId)
                                       .ToListAsync(cancellationToken);

            return favLines
                .OrderBy(favLine => ExtractLeadingNumber(favLine?.Line?.Name ?? ""))
                .ThenBy(favLine => favLine?.Line?.Name ?? "");
        }
        private int? ExtractLeadingNumber(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var match = System.Text.RegularExpressions.Regex.Match(name, @"^\d+");
            return match.Success ? int.Parse(match.Value) : (int?)null;
        }

        public async Task<FavoriteLine> CreateFavoriteLineAsync(FavoriteLine favoriteLine, CancellationToken cancellationToken)
        {
            var lineExists = await context.Lines.AnyAsync(x => x.Id == favoriteLine.LineId,cancellationToken);
            if (!lineExists) { throw new KeyNotFoundException($"Line with Id:{favoriteLine.LineId} not found");}

            var userExists = await context.MyAppUsers.AnyAsync(x => x.Id == favoriteLine.UserId, cancellationToken);
            if (!lineExists) { throw new KeyNotFoundException($"User with Id:{favoriteLine.UserId} not found"); }

            context.FavoriteLines.Add(favoriteLine);
            await context.SaveChangesAsync(cancellationToken);
            return favoriteLine;
        }

        public async Task<bool> DeleteFavoriteLineAsync(int id, CancellationToken cancellationToken)
        {
            var favoriteLine = await context.FavoriteLines.FindAsync(new object[] { id }, cancellationToken);
            if (favoriteLine == null) return false;
            context.FavoriteLines.Remove(favoriteLine);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
