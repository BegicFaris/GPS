
namespace RS1_2024_25.API.Data.Models
{
    //Interface that is used to differentiate classes that use multitenantcy
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
