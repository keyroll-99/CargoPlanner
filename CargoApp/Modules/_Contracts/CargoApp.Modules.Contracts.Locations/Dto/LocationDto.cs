namespace CargoApp.Modules.Locations.Application.DTO;

public class LocationDto
{
    public double Lat { get;  private set; }
    public double Lon { get;  private set; }
    public string DisplayName { get;  private set; }
    public long OsmId { get;  private set;}
    public string Name { get;  private set; }
    public AddressDto Address { get;  private set; }

    public LocationDto(double lat, double lon, string displayName, long osmId, string name, AddressDto address)
    {
        Lat = lat;
        Lon = lon;
        DisplayName = displayName;
        OsmId = osmId;
        Name = name;
        Address = address;
    }
}