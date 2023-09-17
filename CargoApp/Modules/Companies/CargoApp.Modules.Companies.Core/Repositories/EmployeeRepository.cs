using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Companies.Core.DAL;
using CargoApp.Modules.Companies.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Companies.Core.Repositories;

internal class EmployeeRepository : Repository<Employee, CompanyDbContext>, IEmployeeRepository
{
    public EmployeeRepository(CompanyDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public Task<bool> ExistsByEmail(string email)
        => Entities.AnyAsync(x => x.Email == email);
}