using GPS.API.Data.Models;
using GPS.API.Dtos.FeedbackDtos;
using GPS.API.Interfaces;
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

        }
    }

