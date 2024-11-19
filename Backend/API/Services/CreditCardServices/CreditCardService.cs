using GPS.API.Data.Models;
using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.CreditCardServices
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ApplicationDbContext _context;
        public CreditCardService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CreditCard>> GetAllCreditCardsAsync() =>
            await _context.CreditCards.ToListAsync();
        public async Task<CreditCard> GetCreditCardByIdAsync(int id) =>
          await _context.CreditCards.FindAsync(id);
        public async Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard)
        {
            _context.CreditCards.Add(creditCard);
            await _context.SaveChangesAsync();
            return creditCard;
        }

        public async Task<CreditCard> UpdateCreditCardAsync(CreditCard creditCard)
        {
            _context.CreditCards.Update(creditCard);
            await _context.SaveChangesAsync();
            return creditCard;
        }

        public async Task<bool> DeleteCreditCardAsync(int id)
        {
            var creditCard = await _context.CreditCards.FindAsync(id);
            if (creditCard == null) return false;
            _context.CreditCards.Remove(creditCard);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
