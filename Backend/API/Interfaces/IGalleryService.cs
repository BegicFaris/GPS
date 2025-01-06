using GPS.API.Data.Models;
using GPS.API.Dtos.PhotoDtos;

namespace GPS.API.Interfaces
{
    public interface IGalleryService
    {
        Task<IEnumerable<Gallery>> GetAllPhotosAsync();
        Task<Gallery> UploadPhotoAsync(Gallery gallery);
        Task<Gallery> GetPhotoByIdAsync(int id);
        Task<bool> DeletePhotoAsync(int id);
        Task<bool> UpdatePhotoOrderAsync(List<PhotoOrderDto> updatedOrder);
    }
}

