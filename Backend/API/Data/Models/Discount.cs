using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Discount : IMustHaveTenant,ISoftDeletable
    {
        [Key]
        public required int Id { get; set; }
        public required string DiscountName { get; set; }
        public required float DiscountValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? TenantId { get; set; }
    }
}
