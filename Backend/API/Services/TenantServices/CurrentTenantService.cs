using Microsoft.EntityFrameworkCore;
using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
namespace GPS.API.Services.TenantServices
{
    //The service responsible for setting the current tenant, also the reason we needed an additional DbContext
    //It has a String type Id as well as a method that sets the current tenant
    public class CurrentTenantService :ICurrentTenantService
    {
        private readonly TenantDbContext _context;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;
        }
        public string? TenantId { get;  set; }

        public async Task<bool> SetTenant(string tenantId)
        {
            var tenantInfo = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                return true;
            }
            else
            {
                throw new Exception("Invalid tenant ID");
            }

           
        }

    }


}