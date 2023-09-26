using CargoApp.Modules.Companies.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Companies.Core.DAL;

public class CompanyDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CompanyDbContext(DbContextOptions options) : base(options)
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("companies");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}