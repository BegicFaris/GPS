
using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCard>> GetAllCreditCardsAsync();
        Task<CreditCard> GetCreditCardByIdAsync(int id);
        Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard);
        Task<CreditCard> UpdateCreditCardAsync(CreditCard creditCard);
        Task<bool> DeleteCreditCardAsync(int id);
    }
}
