
using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllDiscountsAsync();
        Task<Discount> GetDiscountByIdAsync(int id);
    }
}
