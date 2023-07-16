using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.DAL;

public class UserDbContext : DbContext
{
#pragma warning disable CS8618
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions options) : base(options)
    {
    }
#pragma warning restore CS8618
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}