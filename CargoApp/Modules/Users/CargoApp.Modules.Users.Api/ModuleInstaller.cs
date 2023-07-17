using System.Runtime.CompilerServices;
using CargoApp.Modules.Users.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]

namespace CargoApp.Modules.Users;

internal static class ModuleInstaller
{
    public const string BasePath = "Users";

    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}