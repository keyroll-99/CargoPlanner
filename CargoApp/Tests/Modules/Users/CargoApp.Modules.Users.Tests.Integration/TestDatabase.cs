using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Users.Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Tests.Integration;

internal class TestDatabase : IDisposable
{
    public UserDbContext UserDbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        var dbContextOptions = new DbContextOptionsBuilder<UserDbContext>().UseNpgsql(options.ConnectionString).Options;
        
        UserDbContext = new UserDbContext(dbContextOptions);
    }

    public void Dispose()
    {
        UserDbContext.Database.EnsureDeleted();
        UserDbContext.Dispose();
    }
}