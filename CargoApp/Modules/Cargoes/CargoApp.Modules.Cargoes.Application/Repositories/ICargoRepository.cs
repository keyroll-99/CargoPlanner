using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Cargoes.Domain.Entities;

namespace CargoApp.Modules.Cargoes.Application.Repositories;

public interface ICargoRepository : IRepository<Cargo, Guid>
{
    
}