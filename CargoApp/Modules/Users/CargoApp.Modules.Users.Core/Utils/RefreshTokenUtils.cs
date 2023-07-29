using CargoApp.Modules.Users.Core.Repositories;

namespace CargoApp.Modules.Users.Core.Utils;

// ToDo it's should be service and i have to try it as singleton
internal class RefreshTokenUtils : IRefreshTokenUtils
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RefreshTokenUtils(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }

    public string GenerateRefreshToken(Guid userId)
    {
        return "dupa secret";
    }
}