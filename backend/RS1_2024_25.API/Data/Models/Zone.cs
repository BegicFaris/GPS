using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models
{

    //Test implementation of Multitenantcy, when we start working on this class delete this, and also delete the code in ApplicationDbContext
    public class Zone : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //Implemntation of the IMustHaveTenant interface
        public string TenantId { get; set; }
    }
}
