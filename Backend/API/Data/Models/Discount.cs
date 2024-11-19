using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Discount
    {
        [Key]
        public required int Id { get; set; }
        public required string DiscountName { get; set; }
        public required string DiscountValue { get; set; }
    }
}
