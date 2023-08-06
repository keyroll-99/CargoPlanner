using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim;

internal class NominatimClientFactory : ILocationClientFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NominatimClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ILocationClient Create()
    {
        return _serviceProvider.GetRequiredService<NominatimClient>();
    }
}