using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Security;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Modules.Users.Api")]
namespace CargoApp.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<UserDbContext>();
        services.AddHostedService<DatabaseInitializer<UserDbContext>>();
        services.AddSecurity();
        
        return services;
    }
}