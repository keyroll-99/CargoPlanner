using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repository;

internal class DriverRepository : IDriverRepository
{
    private readonly CargoDbContext _dbContext;
    
    public DriverRepository(CargoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Driver driver)
    {
        _dbContext.Drivers.Add(driver);
        await _dbContext.SaveChangesAsync();
    }
}