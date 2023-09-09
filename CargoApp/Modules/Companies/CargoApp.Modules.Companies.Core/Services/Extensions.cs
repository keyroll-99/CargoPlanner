using CargoApp.Module.Contracts.Companies;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Companies.Core.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompany, CompanyService>();
        
        return services;
    }
}