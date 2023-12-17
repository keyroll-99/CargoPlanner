using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Locations.Application;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Infrastructure.DAL;
using CargoApp.Modules.Locations.Infrastructure.DAL.SeedData;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.Options;
using CargoApp.Modules.Locations.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Modules.Locations.Api")]

namespace CargoApp.Modules.Locations.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<LocationDbContext>();
        services.AddRepositories();
        
        var nominatimSettings = services.GetOptions<NominatimOptions>("Nominatim");
        
        services.AddHttpClient<NominatimClient>(nominatimSettings.ClientName,
            x => { x.BaseAddress = new Uri(nominatimSettings.BaseUri); });
        services.AddSingleton<ILocationClientFactory, NominatimClientFactory>();

        services.AddHostedService<SeedData>();
        
        services.AddApplication();
        return services;
    }
}