using System.Runtime.CompilerServices;
using CargoApp.Modules.Cargoes.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]
namespace CargoApp.Modules.Cargoes.Api;

internal static class ModuleInstaller
{
    public const string BasePath = "Cargoes";


    public static IServiceCollection AddCargoes(this IServiceCollection services)
    {
        services.AddInfrastructure();
        return services;
    }
}