using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Companies.Core.Entities;

namespace CargoApp.Modules.Companies.Core.Repositories;

internal interface ICompanyRepository : IRepository<Company, Guid>
{
    
}