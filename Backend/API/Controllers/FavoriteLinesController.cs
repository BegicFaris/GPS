using GPS.API.Data.Models;
using GPS.API.Dtos.FavoriteLineDtos;
using GPS.API.Interfaces;
using GPS.API.Services.BusServices;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class FavoriteLinesController(IFavoriteLineService favoriteLineService) : MyControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteLines() =>
            Ok(await favoriteLineService.GetAllFavoriteLinesAsync());

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFavoriteLineByUserId(int userId)
        {
            var favoriteLines = await favoriteLineService.GetFavoriteLineByUserIdAsync(userId);
            if (favoriteLines == null) return NotFound();
            return Ok(favoriteLines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavoriteLine(int id)
        {
            var favoriteLine = await favoriteLineService.GetFavoriteLineByIdAsync(id);
            if (favoriteLine == null) return NotFound();
            return Ok(favoriteLine);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoriteLine(FavoriteLineCreateDto favoriteLineCreateDto)
        {
            var favoriteLine = new FavoriteLine
            {
                UserId = favoriteLineCreateDto.UserId,
                LineId = favoriteLineCreateDto.LineId
            };
            var createdFavoriteLine = await favoriteLineService.CreateFavoriteLineAsync(favoriteLine);
            return CreatedAtAction(nameof(CreateFavoriteLine), new { id = createdFavoriteLine.Id }, createdFavoriteLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var success = await favoriteLineService.DeleteFavoriteLineAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
