using System.Runtime.CompilerServices;
using CargoApp.Modules.Planner.Application;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]
namespace CargoApp.Modules.Planner.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddPlanner(this IServiceCollection services)
    {
        services.AddApplication();
        return services;
    }
}