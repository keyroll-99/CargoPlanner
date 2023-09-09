using CargoApp.Core.ShareCore.Enums;

namespace CargoApp.Core.Abstraction.Auth;

public interface IAuthManager
{
    JsonWebToken CreateToken(Guid userId, string email, PermissionEnum permission, Guid companyId);
}