using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repository;

internal class CargoRepository : ICargoRepository
{
   private readonly CargoDbContext _dbContext;
   
   public CargoRepository(CargoDbContext cargoDbContext)
   {
      _dbContext = cargoDbContext;
   }
}