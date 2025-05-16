using ZenBook_Backend.Service;
using ZenBook_Backend.Shared;


namespace ZenBook_Backend.Middleware
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
            context.Request.Headers.TryGetValue("x-tenant-id", out var tenantFromHeader);
            if (!string.IsNullOrEmpty(tenantFromHeader))
            {

               var tenantSetSuccessfully = await currentTenantService.SetTenant(tenantFromHeader);
                if (!tenantSetSuccessfully)
                {
                    var result = new Result<string> { Data = null, IsSuccess = false, Error = new ErrorModel("invalid_tenant", "Invalid tenant") };
                    await context.Response.WriteAsJsonAsync(result);
                    return;
                }
            }
            await _next(context);
        }

    }
}
