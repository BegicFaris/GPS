using GPS.API.Data.Models;
using GPS.API.Dtos.FeedbackDtos;
using GPS.API.Interfaces;
using GPS.API.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class DiscountsController:MyControllerBase
    {

            private readonly IDiscountService _discountService;

            public DiscountsController(IDiscountService discountService)
            {
                _discountService = discountService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllDiscounts() =>
                Ok(await _discountService.GetAllDiscountsAsync());

            [HttpGet("{id}")]
            public async Task<IActionResult> GetDiscount(int id)
            {
                var discount = await _discountService.GetDiscountByIdAsync(id);
                if (discount == null) return NotFound();
                return Ok(discount);
            }


        [HttpPost]
        public async Task<IActionResult> CreateDiscount(Discount discount)
        {
            var createdDiscount = await _discountService.CreateDiscountAsync(discount);
            return CreatedAtAction(nameof(GetDiscount), new { id = createdDiscount.Id }, createdDiscount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, Discount discount)
        {
            if (id != discount.Id) return BadRequest();
            var updatedDiscount = await _discountService.UpdateDiscountAsync(discount);
            return Ok(updatedDiscount);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var success = await _discountService.DeleteDiscountAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

    }
    }

