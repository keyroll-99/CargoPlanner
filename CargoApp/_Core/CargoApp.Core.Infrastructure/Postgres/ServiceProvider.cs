using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;

namespace CargoApp.Core.Infrastructure.Postgres;

internal class ServiceProvider : IServiceProvider
{
    private readonly System.IServiceProvider _serviceProvider;

    public ServiceProvider(System.IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // Factory for schedulers
    public async Task<TService> GetService<TService>() where TService : notnull
    {
        var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<TService>();
    }
}