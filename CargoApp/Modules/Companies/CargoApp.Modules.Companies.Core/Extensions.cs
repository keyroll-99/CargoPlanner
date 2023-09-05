using System.Reflection;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Companies.Core.DAL;
using CargoApp.Modules.Companies.Core.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Companies.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<CompanyDbContext>();

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddPolicies(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}