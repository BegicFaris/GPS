using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.TenantServices
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _context;

        public TenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync(CancellationToken cancellationToken)
        {
            return await _context.Tenants
                .ToListAsync(cancellationToken);
        }
    }
}
