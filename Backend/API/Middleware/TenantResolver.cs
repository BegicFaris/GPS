using Azure.Core;
using GPS.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace GPS.API.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;
        private readonly Dictionary<int, string> _tenantMappings = new()
        {
            { 4200, "mostar" },
            { 4202, "sarajevo" }
        };
        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
    
            var path = context.Request.Path.Value;
            if (!string.IsNullOrEmpty(path) && path.StartsWith("/api"))
            {
                var t=GetTenantFromRequest(context);
                await currentTenantService.SetTenant(t);
                await _next(context);
                return;
            }

            string? tenantIdFromUrl = GetTenantFromPort(context);
            string? tenantIdFromRequest = GetTenantFromRequest(context);

            if (!string.IsNullOrEmpty(tenantIdFromUrl) && !string.IsNullOrEmpty(tenantIdFromRequest))
            {
                if (tenantIdFromUrl != tenantIdFromRequest)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Tenant ID mismatch between URL and request.");
                    return;
                }
                await currentTenantService.SetTenant(tenantIdFromUrl);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID is missing in the URL or request.");
                return;
            }
            await _next(context);
        }
        private string? GetTenantFromPort(HttpContext context)
        {
            var port = context.Request.Host.Port ?? 0;
            return _tenantMappings.ContainsKey(port) ? _tenantMappings[port] : null;
        }
        private string? GetTenantFromRequest(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken.Claims.FirstOrDefault(c => c.Type == "TenantId")?.Value;
            }

            return context.Request.Headers["Tenant"].FirstOrDefault();
        }



    }



}
