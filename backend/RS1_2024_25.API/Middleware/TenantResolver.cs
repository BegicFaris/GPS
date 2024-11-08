using RS1_2024_25.API.Services.TenantServices;

namespace RS1_2024_25.API.Middleware
{
    public class TenantResolver(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("tenantId", out var tenantFromHeader);
            if (string.IsNullOrEmpty(tenantFromHeader) == false) 
            {
                await currentTenantService.SetTenant(tenantFromHeader);
            }
            await next(context);
        }
    }
}
