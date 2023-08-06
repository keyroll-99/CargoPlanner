namespace CargoApp.Modules.Locations.Application.DTO;

public record LocationDto
{
    public required double Lat { get; init; }
    public required double Lon { get; init; }
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
    public required long OsmId { get; init;}
}