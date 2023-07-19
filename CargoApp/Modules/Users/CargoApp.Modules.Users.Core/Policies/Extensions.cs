using System.Reflection;
using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Policies;

internal static class Extensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        foreach (var policies in GetAllPolicy<CreateUserCommand>())
        {
            services.Add(new ServiceDescriptor(
                typeof(IPolicy<CreateUserCommand>),
                policies,
                ServiceLifetime.Scoped));
        }

        return services;
    }

    private static IEnumerable<Type> GetAllPolicy<T>()
        => Assembly.GetExecutingAssembly().GetTypes().Where(x =>
            x is { IsAbstract: false, IsClass: true } && typeof(IPolicy<T>).IsAssignableFrom(x));
}