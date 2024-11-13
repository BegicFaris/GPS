using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    [Table("Managers")]
    public class Manager:MyAppUser
    {
        public DateTime HireDate { get; set; }
        public string Department {  get; set; }
        public string ManagerLevel { get; set; }
    }
}
