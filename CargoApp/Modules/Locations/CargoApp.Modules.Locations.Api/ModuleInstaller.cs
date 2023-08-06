using System.Runtime.CompilerServices;
using CargoApp.Modules.Locations.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]
namespace CargoApp.Modules.Locations.Api;

internal static class ModuleInstaller
{
    public static string ModuleName => "Location";

    public static IServiceCollection AddLocations(this IServiceCollection services)
    {
        services.AddInfrastructure();
        return services;
    }
    
}