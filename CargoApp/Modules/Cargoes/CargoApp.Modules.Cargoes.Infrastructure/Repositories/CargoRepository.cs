using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Application.Repositories;
using CargoApp.Modules.Cargoes.Domain.Entities;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repositories;

public class CargoRepository : Repository<Cargo, CargoDbContext>, ICargoRepository
{
    protected CargoRepository(CargoDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }
}