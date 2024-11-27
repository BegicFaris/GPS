using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class TenantController: MyControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }


        [HttpGet("tenant")]
        public async Task<IActionResult> GetAllTenants() =>
            Ok(await _tenantService.GetAllTenantsAsync());
    }
}
