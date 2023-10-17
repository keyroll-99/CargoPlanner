using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL;

internal class CargoDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CargoDbContext(DbContextOptions<CargoDbContext> options) : base(options)
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    
    public DbSet<Cargo> Cargoes { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Company> Companies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cargoes");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}