using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Companies.Core.DAL;
using CargoApp.Modules.Companies.Core.Entities;

namespace CargoApp.Modules.Companies.Core.Repositories;

internal class WorkerRepository : Repository<Employee, CompanyDbContext>, IWorkerRepository
{
    public WorkerRepository(CompanyDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }
}