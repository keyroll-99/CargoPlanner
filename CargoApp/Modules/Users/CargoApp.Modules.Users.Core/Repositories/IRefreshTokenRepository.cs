using System.Linq.Expressions;
using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

internal interface IRefreshTokenRepository : IRepository<RefreshToken, Guid>
{
    Task<bool> TokenExistsAsync(string token);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<List<RefreshToken>> GetAllTokenByUserIdAsync(Guid userId,  Expression<Func<RefreshToken, bool>>? additionalFilter = null);
    Task RevokeAllUserTokens(Guid userId);
}