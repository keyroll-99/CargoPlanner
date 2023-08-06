using CargoApp.Modules.Locations.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Locations.Infrastructure.DAL;

public class LocationDbContext : DbContext
{
    public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
    {
    }

    protected LocationDbContext()
    {
    }

    public DbSet<Location> Locations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("locations");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}