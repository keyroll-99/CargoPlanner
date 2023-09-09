using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.ShareCore.Enums;

namespace CargoApp.Core.TestCore;

public class TestIdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; set; }
    public Guid Id { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    public PermissionEnum Permissions { get; set; }
    public Guid CompanyId { get; set; }
}