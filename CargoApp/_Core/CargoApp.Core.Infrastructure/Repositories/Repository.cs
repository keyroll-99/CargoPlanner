using System.Collections;
using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Entites;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Core.Infrastructure.Repositories;

public class Repository<TModel, TAppContext> : IRepository<TModel, Guid>
    where TModel : BaseEntity
    where TAppContext : DbContext
{
    protected readonly IClock Clock;
    protected readonly TAppContext AppContext;
    protected readonly DbSet<TModel> Entities;

    protected Repository(TAppContext appContext, IClock clock)
    {
        AppContext = appContext;
        Clock = clock;
        Entities = appContext.Set<TModel>();
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public Task<TModel?> GetByIdAsync(Guid id)
    {
        return Entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistsById(Guid id)
        => Entities.AnyAsync(x => x.Id == id);
    public async Task<TModel> AddAsync(TModel model)
    {
        if (model.Id == Guid.Empty)
        {
            model.Id = Guid.NewGuid();
        }
        model.CreateAt = Clock.Now();
        await Entities.AddAsync(model);
        await AppContext.SaveChangesAsync();
        return model;
    }

    public async Task<TModel> UpdateAsync(TModel model)
    {
        Entities.Update(model);
        await AppContext.SaveChangesAsync();
        return model;
    }

    public async Task<bool> DeleteAsync(TModel model)
    {
        Entities.Remove(model);
        return await AppContext.SaveChangesAsync() > 0;
    }
}