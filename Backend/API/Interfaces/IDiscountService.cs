
using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllDiscountsAsync(CancellationToken cancellationToken);
        Task<Discount?> GetDiscountByIdAsync(int id, CancellationToken cancellationToken);
        Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken cancellationToken);
        Task<Discount> UpdateDiscountAsync(Discount discount, CancellationToken cancellationToken);
        Task<bool> DeleteDiscountAsync(int id, CancellationToken cancellationToken);
    }
}
