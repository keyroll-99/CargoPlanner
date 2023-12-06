using CargoApp.Core.Abstraction.Auth;
using Result.ApiResult;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IRefreshTokenService
{
    Task<string> GenerateTokenAsync(Guid userId);
    Task<ApiResult<string, string>> RefreshTokenAsync(string token);
    Task<ApiResult<JsonWebToken, string>> GenerateJsonWebTokenAsync(string token);
}