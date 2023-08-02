namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IRefreshTokenService
{
    Task<string> GenerateTokenAsync(Guid userId);
    Task<string> RefreshTokenAsync(string token);
    Task InvokeAllRefreshTokenAsync(Guid userId);
}