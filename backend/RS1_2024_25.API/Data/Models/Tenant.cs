using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{

    //The class responsible for creating multitenantcy, it has a string type Id that doesent autogenerate and a name
    //When the  IMustHaveTenant  interface TenantId and this Id are the same it menas that particulat tenant is the current tenant and will show up in DB querries
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

// How it works
// When we get a request for some data it first passes to the TenantResolver middleware that checks what tenant has been pased in the header 
// Then it goes to the tenant service and it will see if the tenant exsists or not and the SetTenant methode will return true
// Then it goes to the request it is intended for and then the AppDbcontext will instantiate with the relevant Tenant which then filters querries
//