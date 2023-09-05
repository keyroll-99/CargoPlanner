using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Cargoes.Core.Entities;

namespace CargoApp.Modules.Cargoes.Application.Repositories;

public interface ICargoRepository : IRepository<Cargo, Guid>
{
    
}