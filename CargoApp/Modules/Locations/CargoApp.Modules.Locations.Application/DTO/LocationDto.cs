namespace CargoApp.Modules.Locations.Application.DTO;

public class LocationDto
{
    public double Lat { get; init; }
    public double Lon { get; init; }
    public string DisplayName { get; init; }
    public long OsmId { get; init;}
    public string name { get; init; }
    public AddressDto Address { get; init; }

    public LocationDto(double lat, double lon, string displayName, long osmId, string name, AddressDto address)
    {
        Lat = lat;
        Lon = lon;
        DisplayName = displayName;
        OsmId = osmId;
        this.name = name;
        Address = address;
    }
}