namespace CargoApp.Modules.Users.Core.Utils;

internal interface IRefreshTokenUtils
{
    string GenerateRefreshToken(Guid userId);
}