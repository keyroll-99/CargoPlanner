using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

internal class PasswordRecoveryRepository : Repository<PasswordRecovery, UserDbContext>, IPasswordRecoveryRepository
{
    public PasswordRecoveryRepository(UserDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }
}