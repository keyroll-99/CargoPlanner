using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    
}