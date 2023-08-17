using System.Reflection;
using CargoApp.Core.ShareCore.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Policies;

public static class Extensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services, Assembly assembly)
    {
        var policyTypes = GetAllPolicesTypes(assembly);
        foreach (var policyType in policyTypes)
        {
            var concretePolicies = GetAllPolicy(assembly, policyType);
            foreach (var concretePolicy in concretePolicies)
                services.Add(new ServiceDescriptor(
                    policyType,
                    concretePolicy,
                    ServiceLifetime.Scoped));
        }

        return services;
    }

    private static IEnumerable<Type> GetAllPolicesTypes(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(x => x is { IsAbstract: false, IsClass: true } && typeof(IPolicyMarker).IsAssignableFrom(x)).ToList()
            .SelectMany(z => z.GetInterfaces().Where(x => x.IsGenericType)).Distinct();
    }

    private static IEnumerable<Type> GetAllPolicy(Assembly assembly, Type type)
    {
        return assembly.GetTypes().Where(x =>
            x is { IsAbstract: false, IsClass: true } && type.IsAssignableFrom(x));
    }
}