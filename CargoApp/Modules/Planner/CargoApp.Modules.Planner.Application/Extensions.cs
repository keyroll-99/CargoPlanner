using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure;
using CargoApp.Core.Infrastructure.Jobs;
using CargoApp.Modules.Planner.Application.Abstract;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Modules.Planner.Infrastructure")]
namespace CargoApp.Modules.Planner.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IExternalServiceFactory, ExternalService>();
        var options = services.GetOptions<PlannerOptions>("Planner");
        services.AddSingleton(options);
        services.AddJob<PlannerScheduler>(options.CronExpression);
        return services;
    }
}