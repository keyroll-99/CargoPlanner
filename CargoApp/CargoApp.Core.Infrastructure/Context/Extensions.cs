using CargoApp.Core.Abstraction.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Context;

public static class Extensions
{
    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IContextFactory, ContextFactory>();

        services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());

        return services;
    }
}