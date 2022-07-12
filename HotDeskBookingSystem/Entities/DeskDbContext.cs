using Microsoft.EntityFrameworkCore;

namespace HotDeskBookingSystem.Entities
{
    public class DeskDbContext : DbContext
    {
        public DeskDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}