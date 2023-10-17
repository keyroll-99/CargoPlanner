using CargoApp.Modules.Locations.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Locations.Infrastructure.DAL;

internal class LocationDbContext : DbContext
{
    public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("locations");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}