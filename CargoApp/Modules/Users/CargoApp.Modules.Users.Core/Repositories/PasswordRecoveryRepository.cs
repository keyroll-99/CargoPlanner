using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Repositories;

internal class PasswordRecoveryRepository : Repository<PasswordRecovery, UserDbContext>, IPasswordRecoveryRepository
{
    public PasswordRecoveryRepository(UserDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public new async Task<IEnumerable<PasswordRecovery>> GetAllAsync()
    {
        return await Entities.Include(x => x.User).ToListAsync();
    }

    public new Task<PasswordRecovery?> GetByIdAsync(Guid id)
    {
        return Entities.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
    }
}