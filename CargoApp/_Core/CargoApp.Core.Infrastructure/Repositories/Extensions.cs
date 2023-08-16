using CargoApp.Core.Abstraction.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositoryFactory(this IServiceCollection services)
    {
        services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
        return services;
    }
}