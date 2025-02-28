using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Line : IMustHaveTenant, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CompleteDistance { get; set; }
        public required bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? TenantId { get; set; }
    }
}
