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
        await _dbContext.AddAsync(company);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        _dbContext.Update(company);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Company?> GetByCompanyId(Guid companyId)
    {
        return _dbContext.Companies.Include("_drivers").AsNoTracking().FirstOrDefaultAsync(x => x.CompanyId == companyId);
    }
}