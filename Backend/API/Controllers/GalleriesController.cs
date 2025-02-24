using GPS.API.Data.Models;
using GPS.API.Dtos.PhotoDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class GalleryController : MyControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto([FromBody] Gallery gallery, CancellationToken cancellationToken)
        {
            if (gallery == null || gallery.PhotoData == null || gallery.PhotoData.Length == 0)
            {
                return BadRequest("Invalid photo data.");
            }

            var uploadedGallery = await _galleryService.UploadPhotoAsync(gallery, cancellationToken);
            return CreatedAtAction(nameof(GetPhotoById), new { id = uploadedGallery.Id }, uploadedGallery);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhotoById(int id, CancellationToken cancellationToken)
        {
            var gallery = await _galleryService.GetPhotoByIdAsync(id, cancellationToken);
            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPhotos(CancellationToken cancellationToken)
        {
            var galleries = await _galleryService.GetAllPhotosAsync(cancellationToken);
            return Ok(galleries);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id, CancellationToken cancellationToken)
        {
            var success = await _galleryService.DeletePhotoAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize]
        [HttpPut("order")]
        public async Task<IActionResult> UpdatePhotoOrder([FromBody] List<PhotoOrderDto> updatedOrder, CancellationToken cancellationToken)
        {
            if (updatedOrder == null || !updatedOrder.Any())
            {
                return BadRequest("Invalid photo order data.");
            }

            // Update the photo order
            var success = await _galleryService.UpdatePhotoOrderAsync(updatedOrder, cancellationToken);
            if (!success)
            {
                return NotFound("Some photos were not found.");
            }

            return Ok(new { message = "Photo order updated successfully." });
        }
    }
}
