using AMVTravels.Models.Configurations;
using AMVTravels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace AMVTravels.Contexts
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }

    //!Clase soporte para updapear el modelo -- fix AppDbContext.

    //public class YourDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    //{
    //    public AppDbContext CreateDbContext(string[] Args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    //        optionsBuilder.UseSqlServer("Server=CNV-158; DataBase=AMVT; Trusted_Connection=True; TrustServerCertificate=True");
    //        return new AppDbContext(optionsBuilder.Options);
    //    }
    //}
}
