using Carware.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Carware.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Customer)
                .WithOne(c => c.Car)
                .HasForeignKey<Car>(c => c.CustomerId);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Seller)
                .WithMany(a => a.CarsSold)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Supervisor)
                .WithMany(Supervisor => Supervisor.EmployeesSupervised)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
