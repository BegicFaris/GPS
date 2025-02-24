using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TenantsController: MyControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }


        [HttpGet("tenant")]
        public async Task<IActionResult> GetAllTenants(CancellationToken cancellationToken) =>
            Ok(await _tenantService.GetAllTenantsAsync(cancellationToken));
    }
}
