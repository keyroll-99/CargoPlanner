﻿using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Entites;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Core.Infrastructure.Repositories;

public class Repository<TModel, TAppContext> : IRepository<TModel, Guid>
    where TModel : BaseEntity
    where TAppContext : DbContext
{
    protected readonly IClock _clock;
    protected readonly TAppContext AppContext;
    protected readonly DbSet<TModel> Entities;

    protected Repository(TAppContext appContext, IClock clock)
    {
        AppContext = appContext;
        _clock = clock;
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

    public async Task<TModel> CreateAsync(TModel model)
    {
        if (model.Id == Guid.Empty)
        {
            model.Id = Guid.NewGuid();
        }
        model.CreateAt = _clock.Now();
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

    public async Task<IList<TModel>> UpdateRangeAsync(IList<TModel> models)
    {
        Entities.UpdateRange(models);
        await AppContext.SaveChangesAsync();
        return models;
    }

    public async Task<bool> DeleteAsync(TModel model)
    {
        Entities.Remove(model);
        return await AppContext.SaveChangesAsync() > 0;
    }
}