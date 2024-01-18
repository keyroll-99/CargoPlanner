using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure;
using CargoApp.Modules.Planner.Application;
using CargoApp.Modules.Planner.Application.Abstract;
using CargoApp.Modules.Planner.Core.Planner.ExternalService;
using CargoApp.Modules.Planner.Infrastructure.ExternalService;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]
namespace CargoApp.Modules.Planner.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddPlanner(this IServiceCollection services)
    {
        var options = services.GetOptions<PlannerOptions>("Planner");
        services.AddHttpClient<OsrmClient>(x =>
        {
            x.BaseAddress = new Uri(options.RouteServiceUrl);
        });

        services.AddSingleton<IRouteEngine, OsrmClient>();
        services.AddSingleton<IRouteEngineFactory, OsrmClientFactory>();
        
        services.AddApplication();
        return services;
    }
}