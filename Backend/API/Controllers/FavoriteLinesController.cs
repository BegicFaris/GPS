using GPS.API.Data.Models;
using GPS.API.Dtos.FavoriteLineDtos;
using GPS.API.Interfaces;
using GPS.API.Services.BusServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class FavoriteLinesController(IFavoriteLineService favoriteLineService) : MyControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteLines(CancellationToken cancellationToken, bool includeDeleted = false) =>
            Ok(await favoriteLineService.GetAllFavoriteLinesAsync(cancellationToken,includeDeleted));
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFavoriteLineByUserId(int userId, CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var favoriteLines = await favoriteLineService.GetFavoriteLineByUserIdAsync(userId, cancellationToken,includeDeleted);
            if (favoriteLines == null) return NotFound();
            return Ok(favoriteLines);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavoriteLine(int id, CancellationToken cancellationToken)
        {
            var favoriteLine = await favoriteLineService.GetFavoriteLineByIdAsync(id, cancellationToken);
            if (favoriteLine == null) return NotFound();
            return Ok(favoriteLine);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFavoriteLine(FavoriteLineCreateDto favoriteLineCreateDto, CancellationToken cancellationToken)
        {
          

            var favoriteLine = new FavoriteLine
            {
                UserId = favoriteLineCreateDto.UserId,
                LineId = favoriteLineCreateDto.LineId
            };
            var createdFavoriteLine = await favoriteLineService.CreateFavoriteLineAsync(favoriteLine,cancellationToken);
            return CreatedAtAction(nameof(CreateFavoriteLine), new { id = createdFavoriteLine.Id }, createdFavoriteLine);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteLine(int id, CancellationToken cancellationToken)
        {
            var success = await favoriteLineService.DeleteFavoriteLineAsync(id,cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
