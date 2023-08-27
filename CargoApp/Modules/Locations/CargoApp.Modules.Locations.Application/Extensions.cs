using System.Reflection;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;
using CargoApp.Modules.Locations.Application.Queries.GetAllLocations;
using CargoApp.Modules.Locations.Application.Queries.SearchLocations;
using CargoApp.Modules.Locations.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Locations.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddLocationCommandHandler, AddLocationCommandHandler>();
        services.AddScoped<IGetAllLocationHandler, GetAllLocationHandler>();
        services.AddSingleton<ISearchLocationQueryHandler, SearchLocationQueryHandler>();
        services.AddPolicies(Assembly.GetExecutingAssembly());
        return services;
    }
}