using Microsoft.EntityFrameworkCore;
using ZenBook_Backend.Models;

namespace ZenBook_Backend.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        { 
        }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
