using CargoApp.Core.Abstraction.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Postgres;

internal class RepositoryProvider : IRepositoryProvider
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // Factory for schedulers
    public async Task<TRepository> GetRepository<TRepository>() where TRepository : notnull
    {
        var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<TRepository>();
    }
}