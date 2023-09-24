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
}