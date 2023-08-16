using CargoApp.Modules.Locations.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Infrastructure.Repositories;

internal static class Extensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILocationRepository, LocationRepository>();
        return services;
    }
}