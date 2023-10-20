namespace CargoApp.Modules.Companies.Core.Entities;

[Flags]
public enum WorkingPositionEnum
{
    Owner = 1 << 0,
    Driver = 1 << 1,
    Dispatcher = 1 << 2
}