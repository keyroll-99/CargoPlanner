using System.Linq.Expressions;
using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Repositories;

public class RefreshTokenRepository : Repository<RefreshToken, UserDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(UserDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public Task<bool> TokenExistsAsync(string token)
    {
        return Entities.AnyAsync(x => x.Token == token);
    }

    public Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return Entities.FirstOrDefaultAsync(x => x.Token == token);
    }

    public Task<List<RefreshToken>> GetAllTokenByUserIdAsync(Guid userId, Expression<Func<RefreshToken, bool>>? additionalFilter = null)
    {
        var result = Entities.Where(x => x.UserId == userId);
        if (additionalFilter is not null)
        {
            result.Where(additionalFilter);
        }

        return result.ToListAsync();
    }
}