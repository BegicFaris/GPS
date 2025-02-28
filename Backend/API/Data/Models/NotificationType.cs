using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class NotificationType : IMustHaveTenant,ISoftDeletable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? TenantId { get; set; }
    }
}
