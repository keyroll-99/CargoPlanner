using CargoApp.Modules.Cargoes.Api;
using CargoApp.Modules.Users;

namespace CargoApp.Bootstrap;

internal static class Modules
{
    public static IServiceCollection LoadModules(this IServiceCollection services)
    {
        services.AddUsers();
        // services.AddCargoes();
        return services;
    }
}