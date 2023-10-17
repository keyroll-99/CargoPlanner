using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Cargoes.Application;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;
using CargoApp.Modules.Cargoes.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Cargoes.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddPostgres<CargoDbContext>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}