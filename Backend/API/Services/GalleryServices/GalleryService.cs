using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos.PhotoDtos;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly ApplicationDbContext _context;

        public GalleryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Upload photo and save to the database
        public async Task<Gallery> UploadPhotoAsync(Gallery gallery)
        {
            if (gallery == null) throw new ArgumentNullException(nameof(gallery));

            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();
            return gallery;
        }

        // Retrieve photo by ID
        public async Task<Gallery> GetPhotoByIdAsync(int id)
        {
            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(g => g.Id == id);

            return gallery;
        }

        // Retrieve all photos
        public async Task<IEnumerable<Gallery>> GetAllPhotosAsync()
        {
            return await _context.Galleries.ToListAsync();
        }

        // Delete photo by ID
        public async Task<bool> DeletePhotoAsync(int id)
        {
            var gallery = await _context.Galleries.FirstOrDefaultAsync(g => g.Id == id);
            if (gallery == null)
            {
                return false;
            }

            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePhotoOrderAsync(List<PhotoOrderDto> updatedOrder)
        {
            // Fetch photos based on the provided IDs
            var photoIds = updatedOrder.Select(o => o.Id).ToList();
            var photosToUpdate = await _context.Galleries
                .Where(p => photoIds.Contains(p.Id))
                .ToListAsync();

            if (photosToUpdate.Count != updatedOrder.Count)
            {
                return false; // Some photos not found
            }

            // Update the position of each photo
            foreach (var photo in photosToUpdate)
            {
                var orderUpdate = updatedOrder.First(o => o.Id == photo.Id);
                photo.Position = orderUpdate.NewPosition;
            }

            // Save the changes
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
