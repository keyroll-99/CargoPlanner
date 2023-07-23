using System.Reflection;
using CargoApp.Core.Abstraction.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Policies;

internal static class Extensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        var policyTypes = GetAllPolicesTypes();
        foreach (var policyType in policyTypes)
        {
            var concretePolicies = GetAllPolicy(policyType);
            foreach (var concretePolicy in concretePolicies)
                services.Add(new ServiceDescriptor(
                    policyType,
                    concretePolicy,
                    ServiceLifetime.Scoped));
        }

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