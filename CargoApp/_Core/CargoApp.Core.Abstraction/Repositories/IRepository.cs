namespace CargoApp.Core.Abstraction.Repositories;

public interface IRepository<TModel, in TId>
{
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(TId id);
    Task<TModel> CreateAsync(TModel model);
    Task<TModel> UpdateAsync(TModel model);
    Task<IList<TModel>> UpdateRangeAsync(IList<TModel> models);
    Task<bool> DeleteAsync(TModel model);
}