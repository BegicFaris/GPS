using GPS.API.Data.Models;
using GPS.API.Dtos.FeedbackDtos;
using GPS.API.Interfaces;
using GPS.API.Services.DiscountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class DiscountsController : MyControllerBase
    {

        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts(CancellationToken cancellationToken) =>
                Ok(await _discountService.GetAllDiscountsAsync(cancellationToken));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscount(int id, CancellationToken cancellationToken)
        {
            var discount = await _discountService.GetDiscountByIdAsync(id,cancellationToken);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(Discount discount, CancellationToken cancellationToken)
        {
            var createdDiscount = await _discountService.CreateDiscountAsync(discount, cancellationToken);
            return CreatedAtAction(nameof(GetDiscount), new { id = createdDiscount.Id }, createdDiscount);
        }
        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, Discount discount, CancellationToken cancellationToken)
        {
            if (id != discount.Id) return BadRequest();
            var updatedDiscount = await _discountService.UpdateDiscountAsync(discount, cancellationToken);
            return Ok(updatedDiscount);
        }
        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id, CancellationToken cancellationToken)
        {
            var success = await _discountService.DeleteDiscountAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }

    }
}

