using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Services.DiscountServices
{
    public class DiscountService: IDiscountService
    {
        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync() =>
            await _context.Discounts.ToListAsync();

        public async Task<Discount> GetDiscountByIdAsync(int id) =>
            await _context.Discounts.FindAsync(id);

    }
}
