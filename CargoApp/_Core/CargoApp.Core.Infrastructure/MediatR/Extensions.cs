using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.MediatR;

public static class Extensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, Type type)
    {
        services.AddMediatR(type);
        return services;
    }
}