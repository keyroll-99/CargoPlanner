namespace CargoApp.Modules.Contracts.Events.Companies.Enums;

[Flags]
public enum WorkingPositionEnum
{
    Owner = 1 << 0,
    Driver = 1 << 1,
    Dispatcher = 1 << 2
}