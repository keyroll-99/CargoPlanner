namespace CargoApp.Core.Abstraction.Repositories;

public interface IRepository<TModel, in TId>
{
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(TId id);
    Task<bool> ExistsById(TId id);
    Task<TModel> AddAsync(TModel model);
    Task<TModel> UpdateAsync(TModel model);
    Task<bool> DeleteAsync(TModel model);
}