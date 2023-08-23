namespace CargoApp.Core.Abstraction.Enums;

[Flags]
public enum PermissionEnum : long
{
    Locations = 1L << 0,
    Workers = 1L << 1,
    Cars = 1L << 2,
    Cargoes = 1L << 3,
}

