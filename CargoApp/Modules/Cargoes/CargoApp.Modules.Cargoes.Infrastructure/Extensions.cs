using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Cargoes.Application;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.Planner.RouteEngine;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;
using CargoApp.Modules.Cargoes.Infrastructure.DAL.SeedData;
using CargoApp.Modules.Cargoes.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Cargoes.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<CargoDbContext>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICargoRepository, CargoRepository>();

        services.AddHostedService<SeedData>();
        services.AddHttpClient<IRouteEngine, RouteEngine.RouteEngine>(x =>
            x.BaseAddress = new Uri("http://localhost:8000"));


        services.AddApplication();
        return services;
    }
}