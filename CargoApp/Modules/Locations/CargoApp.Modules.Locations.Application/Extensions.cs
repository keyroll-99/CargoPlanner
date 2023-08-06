using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}