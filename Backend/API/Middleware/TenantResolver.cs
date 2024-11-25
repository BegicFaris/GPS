using Azure.Core;
using GPS.API.Interfaces;

namespace GPS.API.Middleware
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
            if (context.Request.Path.StartsWithSegments("/api/Account/Register/Driver") ||
                context.Request.Path.StartsWithSegments("/api/Account/Login") ||
                    context.Request.Path.StartsWithSegments("/api/AAGetTokenTest/GetToken"))
            {

                await _next(context); // Continue without doing tenant resolution
                return;
            }

            // Extract the TenantId from the JWT token (from the claims)
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var tenantId = context.User?.Claims?.FirstOrDefault(c => c.Type == "TenantId")?.Value;

            if (!string.IsNullOrEmpty(tenantId))
            {
                try
                {
                    // Set the tenant context in the service
                    await currentTenantService.SetTenant(tenantId);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync($"Invalid tenant ID: {ex.Message}");
                    return;
                }
            }
            else
            {
                // Handle the case where the TenantId claim is missing in the JWT token
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("TenantId claim is missing in the JWT token.");
                return;
            }

            await _next(context);
        }
    }



}
