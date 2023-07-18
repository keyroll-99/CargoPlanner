namespace CargoApp.Core.Abstraction.Repositories;

public interface IRepository<TModel, TId>
{
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(TId id);
    Task<TModel> CreateAsync(TModel model);
    Task<TModel> UpdateAsync(TModel model);
    Task<bool> DeleteAsync(TModel model);
}