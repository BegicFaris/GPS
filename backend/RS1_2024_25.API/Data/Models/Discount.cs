using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models
{
    public class Discount
    {
        [Key]
        public required int Id { get; set; }
        public required string DiscountName { get; set; }
        public required string DiscountValue { get; set; }
    }
}
