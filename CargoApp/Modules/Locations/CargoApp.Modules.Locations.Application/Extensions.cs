using CargoApp.Core.Infrastructure.MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Extensions));
        return services;
    }
}