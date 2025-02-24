using Azure.Core;
using GPS.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace GPS.API.Middleware
{
    public class TenantResolver(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private readonly Dictionary<int, string> _tenantMappings = new()
        {
            { 4200, "mostar" },
            { 4202, "sarajevo" }
        };

        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            var cancellationToken = context.RequestAborted;

            var path = context.Request.Path.Value;
            if (!string.IsNullOrEmpty(path) && path.StartsWith("/api"))
            {
                if (path.StartsWith("/api/AAGetTokenTest/GetToken"))
                {
                    await _next(context);
                    return;
                }

                var t = GetTenantFromRequest(context);
                if(t==null)
                {
                    await currentTenantService.SetTenant("mostar", cancellationToken);
                    return;
                }
                await currentTenantService.SetTenant(t,cancellationToken);
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
                await currentTenantService.SetTenant(tenantIdFromUrl, cancellationToken);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID is missing in the URL or request.");
                return;
            }
            if (cancellationToken.IsCancellationRequested)
            {
                context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                await context.Response.WriteAsync("Request was canceled by the client.");
                return;
            }
            await _next(context);
        }
        private string? GetTenantFromPort(HttpContext context)
        {
            var port = context.Request.Host.Port ?? 0;
            return _tenantMappings.TryGetValue(port, out string? value) ? value : null;
        }
        private static string? GetTenantFromRequest(HttpContext context)
        {
            //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
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
