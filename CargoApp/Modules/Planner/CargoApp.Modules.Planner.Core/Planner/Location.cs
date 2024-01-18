namespace CargoApp.Modules.Planner.Core.Planner;

public class Location
{
    public required string Name { get; init; }
    public required double Lat { get; init; }
    public required double Lon { get; init; }
    public required long OsmId { get; init; }
}