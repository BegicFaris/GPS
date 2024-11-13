using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Controllers
{
    public class CreditCardsController : MyEndpointBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCreditCards() =>
            Ok(await _creditCardService.GetAllCreditCardsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditCard(int id)
        {
            var creditCard = await _creditCardService.GetCreditCardByIdAsync(id);
            if (creditCard == null) return NotFound();
            return Ok(creditCard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCreditCard(CreditCard creditCard)
        {
            var createdCreditCard = await _creditCardService.CreateCreditCardAsync(creditCard);
            return CreatedAtAction(nameof(GetCreditCard), new { id = createdCreditCard.Id }, createdCreditCard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCreditCard(int id, CreditCard creditCard)
        {
            if (id != creditCard.Id) return BadRequest();
            var updatedCreditCard = await _creditCardService.UpdateCreditCardAsync(creditCard);
            return Ok(updatedCreditCard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCard(int id)
        {
            var success = await _creditCardService.DeleteCreditCardAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
