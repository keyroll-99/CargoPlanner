using System.Security.Cryptography;
using CargoApp.Core.Abstraction.Clock;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;

    private const int HowManyDaysRefreshTokenValid = 7;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IClock clock)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _clock = clock;
    }

    public async Task<string> GenerateTokenAsync(Guid userId)
    {
        var token = await GetUniqueRefreshToken();
        await _refreshTokenRepository.CreateAsync(RefreshToken.Create(userId, token, _clock));
        return token;
    }

    public Task<string> RefreshTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task InvokeAllRefreshTokenAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    private async Task<string> GetUniqueRefreshToken()
    {
        while (true)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            if (await _refreshTokenRepository.TokenExistsAsync(token))
            {
                continue;
            }
            return token;
        }
    }
}