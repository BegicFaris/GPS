using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models
{
    public class Zone : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string TenantId { get; set; }
    }
}
