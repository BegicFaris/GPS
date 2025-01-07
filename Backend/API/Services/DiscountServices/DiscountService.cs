using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Discount = GPS.API.Data.Models.Discount;

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

        public async Task<Discount> CreateDiscountAsync(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount> UpdateDiscountAsync(Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task<bool> DeleteDiscountAsync(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return false;
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
