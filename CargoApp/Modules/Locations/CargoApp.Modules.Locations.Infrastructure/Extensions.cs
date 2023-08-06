using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Locations.Application;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Infrastructure.DAL;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Modules.Locations.Api")]

namespace CargoApp.Modules.Locations.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<LocationDbContext>();
        services.AddHttpClient<NominatimClient>("NominatimClient",
            // TODO: it's should be in config
            x => { x.BaseAddress = new Uri("https://nominatim.openstreetmap.org"); });
        services.AddSingleton<ILocationClientFactory, NominatimClientFactory>();
        services.AddApplication();
        return services;
    }
}