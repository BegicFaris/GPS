using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos.PhotoDtos;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace GPS.API.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly ApplicationDbContext _context;

        public GalleryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Gallery> UploadPhotoAsync(Gallery gallery, CancellationToken cancellationToken)
        {
            if (gallery == null) throw new ArgumentNullException(nameof(gallery));

            // Assign default position as the next highest position
            var maxPosition = await _context.Galleries.MaxAsync(g => (int?)g.Position, cancellationToken) ?? 0;
            gallery.Position = maxPosition + 1;

            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync(cancellationToken);
            return gallery;
        }

        // Retrieve photo by ID
        public async Task<Gallery?> GetPhotoByIdAsync(int id, CancellationToken cancellationToken)
        {
            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
            return gallery;
        }

        public async Task<IEnumerable<Gallery>> GetAllPhotosAsync(CancellationToken cancellationToken)
        {
            return await _context.Galleries
                .OrderBy(g => g.Position) // Ensure photos are ordered by position
                .ToListAsync(cancellationToken);
        }

        // Delete photo by ID
        public async Task<bool> DeletePhotoAsync(int id, CancellationToken cancellationToken)
        {
            var gallery = await _context.Galleries.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
            if (gallery == null)
            {
                return false;
            }

            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdatePhotoOrderAsync(List<PhotoOrderDto> updatedOrder, CancellationToken cancellationToken)
        {
            // Fetch photos based on the provided IDs
            var photoIds = updatedOrder.Select(o => o.Id).ToList();
            var photosToUpdate = await _context.Galleries
                .Where(p => photoIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

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
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
