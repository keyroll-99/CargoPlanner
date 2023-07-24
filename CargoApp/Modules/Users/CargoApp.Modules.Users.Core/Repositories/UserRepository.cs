using CargoApp.Core.Abstraction.Clock;
using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Repositories;

internal class UserRepository : Repository<User, UserDbContext>, IUserRepository
{
    public UserRepository(UserDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Entities.AnyAsync(x => x.Email == email);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return Entities.SingleOrDefaultAsync(x => x.Email == email);
    }
}