using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

internal interface IRefreshTokenRepository : IRepository<RefreshToken, Guid>
{
    Task<bool> TokenExistsAsync(string token);
}