using GPS.API.Data.Models;
using GPS.API.Dtos.PhotoDtos;

namespace GPS.API.Interfaces
{
    public interface IGalleryService
    {
        Task<Gallery> UploadPhotoAsync(Gallery gallery, CancellationToken cancellationToken);
        Task<Gallery?> GetPhotoByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Gallery>> GetAllPhotosAsync(CancellationToken cancellationToken);
        Task<bool> DeletePhotoAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdatePhotoOrderAsync(List<PhotoOrderDto> updatedOrder, CancellationToken cancellationToken);
    }
}

