using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GPS.API.Data.Models
{
    [Table("Passengers")]
    public class Passenger : MyAppUser
    {
        [ForeignKey(nameof(Discount))]
        public int? DiscountID { get; set; }
        [JsonIgnore]
        public Discount? Discount { get; set; }
    }
}
