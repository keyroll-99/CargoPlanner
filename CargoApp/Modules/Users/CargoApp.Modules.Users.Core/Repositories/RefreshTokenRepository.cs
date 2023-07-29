using CargoApp.Core.Abstraction.Clock;
using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Repositories;

public class RefreshTokenRepository : Repository<RefreshToken, UserDbContext>, IRefreshTokenRepository
{
    protected RefreshTokenRepository(UserDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public Task<bool> TokenExistsAsync(string token)
    {
        return Entities.AnyAsync(x => x.Token == token);
    }
}