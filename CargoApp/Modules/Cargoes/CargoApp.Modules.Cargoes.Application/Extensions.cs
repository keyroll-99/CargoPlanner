
using CargoApp.Core.Infrastructure.Rabbit;
using CargoApp.Modules.Cargoes.Application.Company;
using CargoApp.Modules.Cargoes.Application.Driver;
using CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;
using CargoApp.Modules.Contracts.Events.Companies;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Cargoes.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddEventConsumer<CompanyCreateConsumer, CompanyCreateEvent>();
        services.AddEventConsumer<EmployeeCreateConsumer, EmployeeCreateEvent>();

        services.AddScoped<ICreateCargoDomainService, CreateCargoDomainService>();
        
        return services;
    }
}