using System.Security.Cryptography;
using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Module.Contracts.Companies;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthManager _authManager;
    private readonly IClock _clock;
    private readonly ICompany _companyService;

    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IClock clock,
        IAuthManager authManager,
        ICompany companyService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _clock = clock;
        _authManager = authManager;
        _companyService = companyService;
    }

    public async Task<string> GenerateTokenAsync(Guid userId)
    {
        var token = await GetUniqueRefreshToken();
        await _refreshTokenRepository.AddAsync(RefreshToken.Create(userId, token, _clock));
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

        var newTokenTask = await GenerateTokenAsync(dbModel.UserId);
        dbModel.IsUsed = true;
        await _refreshTokenRepository.UpdateAsync(dbModel);

        return Result<string, string>.Success(newTokenTask);
    }

    public async Task<Result<JsonWebToken, string>> GenerateJsonWebTokenAsync(string token)
    {
        var user = (await _refreshTokenRepository.GetByTokenAsync(token))?.User;
        if (user is null)
        {
            return "Token doesn't exists";
        }

        var company = user.EmployeeId.HasValue
            ? await _companyService.FindEmployeeCompany(user.EmployeeId.Value)
            : null;

        return _authManager.CreateToken(user.Id, user.Email, user.PermissionMask, company?.Id ?? Guid.Empty);
    }

    private async Task InvokeAllRefreshTokenAsync(Guid userId)
    {
        await _refreshTokenRepository.RevokeAllUserTokens(userId);
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