using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repository;

internal class CompanyRepository : ICompanyRepository
{
    private readonly CargoDbContext _dbContext;
    
    public CompanyRepository(CargoDbContext cargoDbContext)
    {
        _dbContext = cargoDbContext;
    }

    public Task AddAsync(Company company)
    {
        throw new NotImplementedException();
    }
}