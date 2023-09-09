using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Companies.Core.DAL;
using CargoApp.Modules.Companies.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Companies.Core.Repositories;

internal class CompanyRepository : Repository<Company, CompanyDbContext>, ICompanyRepository
{
    public CompanyRepository(CompanyDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public Task<Company?> GetCompanyByEmployeeId(Guid employeeId)
    {
        return Entities
            .Include(x => x.Employees)
            .Where(x => x.Employees.TrueForAll(employee => employee.Id == employeeId))
            .FirstOrDefaultAsync();
    }
}