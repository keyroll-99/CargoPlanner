using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Users.Core.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<UserDbContext>();
        services.AddHostedService<DatabaseInitializer<UserDbContext>>();
        return services;
    }
}