using GPS.API.Data.Models;
using GPS.API.Dtos.PhotoDtos;
using GPS.API.Interfaces;
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

        // POST: api/gallery/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto([FromBody] Gallery gallery)
        {
            if (gallery == null || gallery.PhotoData == null || gallery.PhotoData.Length == 0)
            {
                return BadRequest("Invalid photo data.");
            }

            var uploadedGallery = await _galleryService.UploadPhotoAsync(gallery);
            return CreatedAtAction(nameof(GetPhotoById), new { id = uploadedGallery.Id }, uploadedGallery);
        }

        // GET: api/gallery/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhotoById(int id)
        {
            var gallery = await _galleryService.GetPhotoByIdAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }

        // GET: api/gallery
        [HttpGet]
        public async Task<IActionResult> GetAllPhotos()
        {
            var galleries = await _galleryService.GetAllPhotosAsync();
            return Ok(galleries);
        }

        // DELETE: api/gallery/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var success = await _galleryService.DeletePhotoAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/gallery/order
        [HttpPut("order")]
        public async Task<IActionResult> UpdatePhotoOrder([FromBody] List<PhotoOrderDto> updatedOrder)
        {
            if (updatedOrder == null || !updatedOrder.Any())
            {
                return BadRequest("Invalid photo order data.");
            }

            // Update the photo order
            var success = await _galleryService.UpdatePhotoOrderAsync(updatedOrder);
            if (!success)
            {
                return NotFound("Some photos were not found.");
            }

            return Ok(new { message = "Photo order updated successfully." });
        }
    }
}
