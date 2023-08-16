namespace CargoApp.Core.Abstraction.Repositories;

public interface IRepositoryFactory
{
    public TRepository GetRepository<TRepository>() where TRepository : notnull;
}