using RS1_2024_25.API.Services.TenantServices;

namespace RS1_2024_25.API.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;

        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            if (context.Request.Headers.TryGetValue("tenantId", out var tenantFromHeader) && !string.IsNullOrEmpty(tenantFromHeader))
            {
                try
                {
                    await currentTenantService.SetTenant(tenantFromHeader);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync($"Invalid tenant ID: {ex.Message}");
                    return;
                }
            }

            await _next(context);
        }
    }
}
