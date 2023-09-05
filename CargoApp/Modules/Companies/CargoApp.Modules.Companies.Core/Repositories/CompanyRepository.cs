using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Companies.Core.DAL;
using CargoApp.Modules.Companies.Core.Entities;

namespace CargoApp.Modules.Companies.Core.Repositories;

internal class CompanyRepository : Repository<Company, CompanyDbContext>, ICompanyRepository
{
    public CompanyRepository(CompanyDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }
}