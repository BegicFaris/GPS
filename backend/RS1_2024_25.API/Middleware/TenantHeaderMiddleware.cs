namespace RS1_2024_25.API.Middleware
{
    public class TenantHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the header is already set; if not, add a default value
            if (!context.Request.Headers.ContainsKey("tenantID"))
            {
                context.Request.Headers["tenantID"] = ""; // Use a default tenant ID
            }

            await _next(context);
        }
    }
}
