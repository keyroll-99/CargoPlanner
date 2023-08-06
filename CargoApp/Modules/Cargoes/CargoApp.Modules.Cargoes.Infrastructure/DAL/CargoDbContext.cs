using CargoApp.Modules.Cargoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL;

public class CargoDbContext : DbContext
{
    public CargoDbContext(DbContextOptions<CargoDbContext> options) : base(options)
    {
    }
    
    public DbSet<Cargo> Cargoes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cargoes");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}