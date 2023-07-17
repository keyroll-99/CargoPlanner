namespace CargoApp.Core.Abstraction.Repositories;

public interface IRepository<TModel, TId>
{
    Task<IEnumerable<TModel>> GetAll();
    Task<TModel?> GetById(TId id);
    Task<TModel> Create(TModel model);
    Task<TModel> Update(TModel model);
    Task<bool> Delete(TModel model);
}