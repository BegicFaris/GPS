using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Discount = GPS.API.Data.Models.Discount;
using System.Threading; // Needed for CancellationToken

namespace GPS.API.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync(CancellationToken cancellationToken) =>
            await _context.Discounts.ToListAsync(cancellationToken);

        public async Task<Discount?> GetDiscountByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Discounts.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);  // Added cancellationToken here

        public async Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken cancellationToken)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync(cancellationToken);
            return discount;
        }

        public async Task<Discount> UpdateDiscountAsync(Discount discount, CancellationToken cancellationToken)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync(cancellationToken);
            return discount;
        }

        public async Task<bool> DeleteDiscountAsync(int id, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (discount == null) return false;

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var hasPassengers = await _context.Passengers.AnyAsync(x => x.DiscountID == id, cancellationToken);
                if (hasPassengers)
                {
                    await _context.Passengers
                        .Where(x => x.DiscountID == id)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DiscountID, (int?)null), cancellationToken);
                }

                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
