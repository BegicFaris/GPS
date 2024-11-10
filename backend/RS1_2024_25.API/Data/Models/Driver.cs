using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    [Table("Driver")]
    public class Driver : MyAppUser
    {
        public string License { get; set; }
        public string DriversLicenseNumber { get; set; }
        public DateTime HireDate { get; set; }
        public float WorkingHoursInAWeek { get; set; }
    }
}
