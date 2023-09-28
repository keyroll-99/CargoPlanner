using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

internal interface IPasswordRecoveryRepository : IRepository<PasswordRecovery, Guid>
{
    
}