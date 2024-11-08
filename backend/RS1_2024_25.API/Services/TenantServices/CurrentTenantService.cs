using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Services.TenantServices
{
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