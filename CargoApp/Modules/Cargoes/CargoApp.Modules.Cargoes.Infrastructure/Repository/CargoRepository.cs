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
         .Include(x => x.From)
         .Include(x => x.To)
         .Include(x => x.Sender)
         .Include(x => x.Receiver)
         .Include(x => x.Driver)
         .FirstOrDefaultAsync(x => x.Id == id);
   }

   public Task<List<Cargo>> GetPageAsync(int page, int pageSize)
   {
      return _dbContext
         .Cargoes
         .Include(c => c.From)
         .Include(c => c.To)
         .Include(c => c.Sender)
         .Include(c => c.Receiver)
         .Include(c => c.Driver)
         .Skip(page * pageSize)
         .Take(pageSize).ToListAsync();
   }

   public Task<List<Cargo>> GetAllToPlanForCompany(Guid companyId)
   {
      return _dbContext
         .Cargoes
         .Include(c => c.From)
         .Include(c => c.To)
         .Include(c => c.Sender)
         .Include(c => c.Receiver)
         .Include(c => c.Driver)
         .Where(c => c.Sender.CompanyId == companyId)
         .Where(c => !c.IsCanceled && !c.IsLocked && !c.IsDelivered)
         .ToListAsync();
   }

   public async Task UpdateAsync(Cargo cargo)
   {
      _dbContext.Cargoes.Update(cargo);
      await _dbContext.SaveChangesAsync();
   }
}