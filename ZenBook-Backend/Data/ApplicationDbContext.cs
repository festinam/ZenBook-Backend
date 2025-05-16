using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZenBook_Backend.Models;
using ZenBook_Backend.Service;

namespace ZenBook_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        // 1) Injected service that holds the current TenantId
        private readonly ICurrentTenantService _currentTenantService;

        // Shortcut to grab the current TenantId in filters / save logic
        private string CurrentTenantId => _currentTenantService.TenantId;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentTenantService currentTenantService
        ) : base(options)
        {
            _currentTenantService = currentTenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //
            // 2) Apply a global query filter so that every IMustHaveTenant-backed
            //    DbSet only returns rows for the current tenant.
            //
            //modelBuilder.Entity<Client>()
            //    .HasQueryFilter(c => c.TenantId == CurrentTenantId);

            modelBuilder.Entity<Instructor>()
                .HasQueryFilter(i => i.TenantId == CurrentTenantId);

            modelBuilder.Entity<Course>()
                .HasQueryFilter(c => c.TenantId == CurrentTenantId);

            modelBuilder.Entity<Session>()
                .HasQueryFilter(s => s.TenantId == CurrentTenantId);

            modelBuilder.Entity<Payment>()
                .HasQueryFilter(p => p.TenantId == CurrentTenantId);

            //
            // 3) Your existing EF Core model config
            //
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Instructor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }


        public override int SaveChanges()
        {
            var tenantId = _currentTenantService.TenantId;
            // only stamp when we actually have a tenant
            if (!string.IsNullOrEmpty(tenantId))
            {
                var entries = ChangeTracker
                    .Entries<IMustHaveTenant>()
                    .Where(e => e.State == EntityState.Added
                             || e.State == EntityState.Modified)
                    .ToList();
                foreach (var e in entries)
                    e.Entity.TenantId = tenantId;
            }

            return base.SaveChanges();
        }



        // 5) Your DbSets
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
