using Microsoft.CodeAnalysis.CSharp.Syntax;
using ZenBook_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace ZenBook_Backend.Service
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;


        public CurrentTenantService(TenantDbContext context) 
        {
            _context = context;
        }  

        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantExists = await _context.Tenants.Where(t => t.Id == tenant).AnyAsync();
            if (!tenantExists)
            {
                return false;

            }
            TenantId = tenant;

            return true;
             
        }

    }
}
