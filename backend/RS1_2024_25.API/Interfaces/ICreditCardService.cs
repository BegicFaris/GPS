using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Interfaces
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
