using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    [Table("Drivers")]
    public class Driver : MyAppUser
    {
        public required string License { get; set; }
        public required string DriversLicenseNumber { get; set; }
        public required DateTime HireDate { get; set; }
        public float WorkingHoursInAWeek { get; set; }
    }
}
