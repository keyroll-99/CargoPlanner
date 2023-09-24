namespace CargoApp.Core.Abstraction.Repositories;

public interface IRepositoryProvider
{
    public Task<TRepository> GetRepository<TRepository>();
}