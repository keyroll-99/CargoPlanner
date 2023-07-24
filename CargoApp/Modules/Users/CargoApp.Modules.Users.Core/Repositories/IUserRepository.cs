using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

internal interface IUserRepository : IRepository<User, Guid>
{
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<User?> GetByEmailAsync(string email);
}