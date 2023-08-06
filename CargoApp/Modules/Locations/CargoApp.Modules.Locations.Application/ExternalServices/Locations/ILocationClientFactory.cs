namespace CargoApp.Modules.Locations.Application.ExternalServices.Locations;

internal interface ILocationClientFactory
{
    public ILocationClient Create();
}