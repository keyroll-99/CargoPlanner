using CargoApp.Modules.Locations.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ISearchLocation, SearchLocation>();
        return services;
    }
}