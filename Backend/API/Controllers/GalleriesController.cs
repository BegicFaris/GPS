using GPS.API.Data.Models;
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
    }
}
