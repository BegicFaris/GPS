using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Gallery : IMustHaveTenant
    {
        [Key] 
        public int Id { get; set; }
        public byte[] PhotoData { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string? TenantId { get; set; }
    }
}
