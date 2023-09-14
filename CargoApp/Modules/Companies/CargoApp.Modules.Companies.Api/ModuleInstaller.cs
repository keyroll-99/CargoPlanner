using System.Runtime.CompilerServices;
using CargoApp.Modules.Companies.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]

namespace CargoApp.Modules.Companies.Api;

internal static class ModuleInstaller
{
    public const string BasePath = "Companies";

    public static IServiceCollection AddCompanies(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}