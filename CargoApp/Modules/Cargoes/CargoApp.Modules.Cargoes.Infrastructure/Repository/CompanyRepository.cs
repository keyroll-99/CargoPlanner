using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repository;

internal class CompanyRepository : ICompanyRepository
{
    private readonly CargoDbContext _dbContext;
    
    public CompanyRepository(CargoDbContext cargoDbContext)
    {
        _dbContext = cargoDbContext;
    }

    public async Task AddAsync(Company company)
    {
        _dbContext.Companies.Add(company);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        _dbContext.Companies.Update(company);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Company?> GetByCompanyId(Guid companyId)
    {
        return _dbContext.Companies.Include("_drivers").FirstOrDefaultAsync(x => x.CompanyId == companyId);
    }

    public Task<Company?> GetById(Guid id)
    {
        return _dbContext.Companies.Include("_drivers").FirstOrDefaultAsync(x => x.Id == id);

    }
}