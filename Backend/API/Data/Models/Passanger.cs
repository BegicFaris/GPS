using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Data.Models
{
    [Table("Passengers")]
    public class Passenger : MyAppUser
    {
        [ForeignKey(nameof(Discount))]
        public int DiscountID { get; set; }
        public Discount? Discount { get; set; }
    }
}
