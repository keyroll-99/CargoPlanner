namespace CargoApp.Modules.Contracts.Events.Locations;

public record LocationCreatedEvent(double Lat, double Lon, string Name, long OsmId)
{
    
}