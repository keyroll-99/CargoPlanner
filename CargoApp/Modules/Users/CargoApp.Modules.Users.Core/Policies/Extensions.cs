using System.Reflection;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.ShareCore.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Policies;

internal static class Extensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddPolicies(Assembly.GetExecutingAssembly());
        return services;
    }

    private static IEnumerable<Type> GetAllPolicesTypes()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x is { IsAbstract: false, IsClass: true } && typeof(IPolicyMarker).IsAssignableFrom(x)).ToList()
            .SelectMany(z => z.GetInterfaces().Where(x => x.IsGenericType)).Distinct();
    }

    private static IEnumerable<Type> GetAllPolicy(Type type)
    {
        return Assembly.GetExecutingAssembly().GetTypes().Where(x =>
            x is { IsAbstract: false, IsClass: true } && type.IsAssignableFrom(x));
    }
}