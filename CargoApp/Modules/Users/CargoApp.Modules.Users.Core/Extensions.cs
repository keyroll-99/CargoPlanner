using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}