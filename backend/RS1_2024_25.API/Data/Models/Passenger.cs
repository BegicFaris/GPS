using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    [Table("Passengers")]
    public class Passenger: MyAppUser
    {
        [ForeignKey(nameof(Discount))]
        public int DiscountID { get; set; }
        public Discount Discount { get; set; }
        
        //CardHistory ??
    }
}
