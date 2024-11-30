using GPS.API.Data.Models;
using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.LineServices
{
    public class LineService: ILineService
    {
        private readonly ApplicationDbContext _context;

        public LineService(ApplicationDbContext context)
        {
            _context = context;
        }

        //.Include(l => l.StartingStation).Include(l=>l.EndingStation)    
        public async Task<IEnumerable<Line>> GetAllLinesAsync() =>
             await _context.Lines.Include(x => x.StartingStation).Include(x=>x.EndingStation).ToListAsync();

            
        public async Task<Line> GetLineByIdAsync(int id) =>
            await _context.Lines.Include(x => x.StartingStation).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Line> CreateLineAsync(Line line)
        {
            _context.Lines.Add(line);
            await _context.SaveChangesAsync();
            return line;
        }

        public async Task<Line> UpdateLineAsync(Line line)
        {
            _context.Lines.Update(line);
            await _context.SaveChangesAsync();
            return line;
        }

        public async Task<bool> DeleteLineAsync(int id)
        {
            var line = await _context.Lines.FindAsync(id);
            if (line == null) return false;
            _context.Lines.Remove(line);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
