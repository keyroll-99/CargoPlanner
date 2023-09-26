using System.Runtime.CompilerServices;
using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

internal interface IUserRepository : IRepository<User, Guid>
{
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByEmployeeId(Guid employeeId);
    public Task<Result<User>> AddAsync(EmployeeCreateEvent @event);
}