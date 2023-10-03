using CargoApp.Core.ShareCore.Entites;
using CargoApp.Core.ShareCore.Enums;

namespace CargoApp.Modules.Users.Core.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool IsActive { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; init; } = new List<RefreshToken>();
    public ICollection<PasswordRecovery> PasswordRecoveries { get; init; } = new List<PasswordRecovery>();
    public PermissionEnum PermissionMask { get; set; } 
    public Guid? EmployeeId { get; set; }
    
}