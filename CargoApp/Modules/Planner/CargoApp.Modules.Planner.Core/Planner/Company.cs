namespace CargoApp.Modules.Planner.Core.Planner;

public class Company
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required IList<Driver> Drivers { get; init; }
}