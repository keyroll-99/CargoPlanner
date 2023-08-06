using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Infrastructure.Response;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IRefreshTokenService
{
    Task<string> GenerateTokenAsync(Guid userId);
    Task<Result<string, string>> RefreshTokenAsync(string token);
    Task<Result<JsonWebToken, string>> GenerateJsonWebTokenAsync(string token);
}