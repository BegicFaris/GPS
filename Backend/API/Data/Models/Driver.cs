using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Data.Models
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
