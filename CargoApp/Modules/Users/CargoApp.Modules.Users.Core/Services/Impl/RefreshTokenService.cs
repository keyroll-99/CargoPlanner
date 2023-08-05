using System.Security.Cryptography;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;

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

    public async Task<Result<string, string>> RefreshTokenAsync(string token)
    {
        var dbModel = await _refreshTokenRepository.GetByTokenAsync(token);
        if (dbModel is null)
        {
            return Result<string, string>.Fail("Refresh token doesn't exists");
        }

        if (dbModel.ExpiredAt < _clock.Now())
        {
            return Result<string, string>.Fail("Refresh token has expired");
        }

        if (dbModel.IsUsed)
        {
            await InvokeAllRefreshTokenAsync(dbModel.UserId);
            return Result<string, string>.Fail("Refresh token has been used");
        }
        
        var newTokenTask = GenerateTokenAsync(dbModel.UserId);
        dbModel.IsUsed = true;
        await _refreshTokenRepository.UpdateAsync(dbModel);

        return Result<string, string>.Success(await newTokenTask);
    }

    private async Task InvokeAllRefreshTokenAsync(Guid userId)
    {
        var allNotUsedTokens = await _refreshTokenRepository.GetAllTokenByUserIdAsync(userId, x => !x.IsUsed);
        foreach (var token in allNotUsedTokens)
        {
            token.IsUsed = true;
        }

        await _refreshTokenRepository.UpdateRangeAsync(allNotUsedTokens);
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