using Microsoft.EntityFrameworkCore;
using ZenBook_Backend.Models;

namespace ZenBook_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options) { }

        // DbSet for each entity in the database
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify precision and scale for the 'Amount' property in the 'Payment' entity
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)"); // Precision 18, Scale 2 (e.g., 123456789012345678.99)

            // Prevent cascade delete on foreign keys
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Sessions)  // Make sure Client has the Sessions collection
                .HasForeignKey(s => s.ClientId)  // Explicitly specify the foreign key
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Sessions)  // Make sure Course has the Sessions collection
                .HasForeignKey(s => s.CourseId)  // Explicitly specify the foreign key
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Instructor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade
        }


    }
}
