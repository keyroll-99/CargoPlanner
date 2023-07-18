﻿using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Core.Infrastructure.Repositories;

public class Repository<TModel, TAppContext> : IRepository<TModel, Guid>
    where TModel : BaseEntity
    where TAppContext : DbContext
{
    protected readonly DbSet<TModel> Entities;
    protected readonly TAppContext AppContext;

    public Repository(TAppContext appContext)
    {
        AppContext = appContext;
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
        model.CreateAt = DateTime.Now;
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
        return (await AppContext.SaveChangesAsync()) > 0;
    }
}