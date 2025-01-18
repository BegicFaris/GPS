using GPS.API.Data.Models;
using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace GPS.API.Services.LineServices
{
    public class LineService : ILineService
    {
        private readonly ApplicationDbContext _context;

        public LineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Line>> GetAllLinesAsync(string? lineName = "", string? stationName="")
        {

            var lines = await _context.Lines.ToListAsync();
            string? normalizedLineName = !string.IsNullOrEmpty(lineName) ? NormalizeString(lineName) : null;
            string? normalizedStationName = !string.IsNullOrEmpty(stationName) ? NormalizeString(stationName) : null;
            if (!string.IsNullOrEmpty(normalizedLineName))
                lines = lines.Where(x => NormalizeString(x.Name).Contains(normalizedLineName)).ToList();

            if (!string.IsNullOrEmpty(normalizedStationName))
            {
                var r = await _context.Routes.Include(x => x.Station).ToListAsync();
                var routes = r.Where(x => NormalizeString(x.Station.Name).Contains(normalizedStationName)).ToList();
                var matchingLineIds = routes.Select(route => route.LineId).Distinct().ToList();

                return lines
                     .Where(line => matchingLineIds.Contains(line.Id))
                     .OrderBy(line => ExtractLeadingNumber(line.Name))
                     .ThenBy(line => line.Name);
            }

            return lines.OrderBy(line => ExtractLeadingNumber(line.Name))
                     .ThenBy(line => line.Name);
        }
        private string NormalizeString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str
                .ToLower() // Convert to lowercase
                .Replace("č", "c")
                .Replace("ć", "c")
                .Replace("š", "s")
                .Replace("đ", "d")
                .Replace("ž", "z")
                .Replace("dj", "d")
                .Replace("-", "")
                .Trim();
        }

        private int? ExtractLeadingNumber(string name)
        {
            if (string.IsNullOrEmpty(name)) return null; // Handle empty names
            var match = System.Text.RegularExpressions.Regex.Match(name, @"^\d+"); // Extract leading number
            return match.Success ? int.Parse(match.Value) : (int?)null; // Return null if no leading number
        }

        public async Task<IEnumerable<Line>> GetAllLinesByStationIdAsync(int stationId)
        {
            var routes = await _context.Routes.Where(x => x.StationId == stationId).ToListAsync();
            var allLines = await _context.Lines.ToListAsync();

            var lines = new List<Line>();

            if (routes != null && routes.Any())
            {
                foreach (var route in routes)
                {
                    var line = allLines.SingleOrDefault(x => x.Id == route.LineId);
                    if (line != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            return lines.GroupBy(line => line.Id).Select(group => group.First()).ToList();
        }


        public async Task<Line> GetLineByIdAsync(int id) =>
            await _context.Lines.SingleOrDefaultAsync(x => x.Id == id);

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
