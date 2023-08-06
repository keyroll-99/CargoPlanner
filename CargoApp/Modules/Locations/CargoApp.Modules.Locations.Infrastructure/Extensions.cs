using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Locations.Application;
using CargoApp.Modules.Locations.Infrastructure.DAL;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("CargoApp.Modules.Locations.Api")]
namespace CargoApp.Modules.Locations.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<LocationDbContext>();
        services.AddApplication();
        return services;
    }
}