using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Services.TenantServices
{
    //The service responsible for setting the current tenant, also the reason we needed an additional DbContext
    //It has a String type Id as well as a method that sets the current tenant
    public class CurrentTenantService(TenantDbContext context) :ICurrentTenantService
    { 
        public string? TenantId { get; set; }
        public async Task<bool> SetTenant (string tenant)
        {
            var tenantInfo = await context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            if (tenantInfo != null) { 
                TenantId= tenantInfo.Id;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid Id");
            }
        }
        
    }


}