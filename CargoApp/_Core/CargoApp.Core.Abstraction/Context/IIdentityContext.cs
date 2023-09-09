using CargoApp.Core.ShareCore.Enums;

namespace CargoApp.Core.Abstraction.Context;

public interface IIdentityContext
{
    bool IsAuthenticated { get; }
    public Guid Id { get; }
    Dictionary<string, IEnumerable<string>> Claims { get; }
    public PermissionEnum Permissions { get; }
    public Guid CompanyId { get; }
}