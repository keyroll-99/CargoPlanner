
using System.Reflection;
using CargoApp.Core.Infrastructure.Jobs;
using CargoApp.Core.Infrastructure.Rabbit;
using CargoApp.Modules.Cargoes.Application.Cargo;
using CargoApp.Modules.Cargoes.Application.Company;
using CargoApp.Modules.Cargoes.Application.Driver;
using CargoApp.Modules.Cargoes.Application.Location;
using CargoApp.Modules.Cargoes.Application.Planner;
using CargoApp.Modules.Cargoes.Core.Planner.RouteEngine;
using CargoApp.Modules.Contracts.Cargoes.Services;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Contracts.Events.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Cargoes.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddEventConsumer<CompanyCreateConsumer, CompanyCreateEvent>();
        services.AddEventConsumer<EmployeeCreateConsumer, EmployeeCreateEvent>();
        services.AddEventConsumer<LocationCreatedConsumer, LocationCreatedEvent>();

        services.AddScoped<ICargoService, CargoService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDriverService, DriverService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddJob<PlannerScheduler>("0 * * ? * *");
        
        
        return services;
    }
}