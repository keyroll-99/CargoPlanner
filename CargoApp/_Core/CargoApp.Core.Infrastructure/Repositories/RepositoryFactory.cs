using CargoApp.Core.Abstraction.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Repositories;

internal class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TRepository GetRepository<TRepository>() where TRepository : notnull => _serviceProvider.GetRequiredService<TRepository>();
}