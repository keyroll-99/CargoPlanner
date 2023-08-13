using System.Runtime.CompilerServices;
using CargoApp.Modules.Locations.Api.Controllers;
using CargoApp.Modules.Locations.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]
namespace CargoApp.Modules.Locations.Api;

internal static class ModuleInstaller
{
    public const string BasePath = "Location";

    public static IServiceCollection AddLocations(this IServiceCollection services)
    {
        services.AddInfrastructure();
        return services;
    }
    
}