using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Identity;
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
    
    public async Task<User> CreateUserAsync()
    {
        var passwordManager = new PasswordHasher<User>();
        const string password = "secret";
        const string email = "test@email.com";
        var user = new User
        {
            Email = email,
            Id = Guid.NewGuid(),
            CreateAt = DateTime.UtcNow,
            IsActive = true,
            EmployeeId = Guid.NewGuid(),
            PermissionMask = PermissionEnum.Admin,
            RefreshTokens = new List<RefreshToken>(),
            Password = passwordManager.HashPassword(null, password),
        };
        await UserDbContext.Users.AddAsync(user);
        await UserDbContext.SaveChangesAsync();

        return user;
    }

    public void Dispose()
    {
        UserDbContext.Database.EnsureDeleted();
        UserDbContext.Dispose();
    }
}