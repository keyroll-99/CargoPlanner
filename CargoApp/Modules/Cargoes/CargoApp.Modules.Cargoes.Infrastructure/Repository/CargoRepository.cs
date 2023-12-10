using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repository;

internal class CargoRepository : ICargoRepository
{
   private readonly CargoDbContext _dbContext;
   
   public CargoRepository(CargoDbContext cargoDbContext)
   {
      _dbContext = cargoDbContext;
   }

   public async Task<Cargo> AddAsync(Cargo cargo)
   {
      await _dbContext.Cargoes.AddAsync(cargo);
      await _dbContext.SaveChangesAsync();

      return cargo;
   }

   public Task<Cargo?> GetByIdAsync(Guid id)
   {
      return _dbContext
         .Cargoes
         .Include("_from")
         .Include("_to")
         .Include("_sender")
         .Include("_receiver")
         .Include("_driver")
         .FirstOrDefaultAsync(x => x.Id == id);
   }

   public Task<List<Cargo>> GetPageAsync(int page, int pageSize)
   {
      return _dbContext
         .Cargoes
         .Include("_from")
         .Include("_to")
         .Include("_sender")
         .Include("_receiver")
         .Include("_driver")
         .Skip(page * pageSize)
         .Take(pageSize).ToListAsync();
   }

   public async Task UpdateAsync(Cargo cargo)
   {
      _dbContext.Cargoes.Update(cargo);
      await _dbContext.SaveChangesAsync();
   }
}