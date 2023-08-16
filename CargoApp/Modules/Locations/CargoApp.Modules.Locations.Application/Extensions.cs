using CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;
using CargoApp.Modules.Locations.Application.Queries.SearchLocation;
using CargoApp.Modules.Locations.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAddLocationCommandHandler, AddLocationCommandHandler>();
        services.AddSingleton<ISearchLocationQueryHandler, SearchLocationQueryHandler>();
        return services;
    }
}