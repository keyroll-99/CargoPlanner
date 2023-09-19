using System.Runtime.CompilerServices;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Events;
using CargoApp.Modules.Users.Core.Policies;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Security;
using CargoApp.Modules.Users.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CargoApp.Modules.Users.Api")]
namespace CargoApp.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<UserDbContext>();
        services.AddRepositories();
        services.AddPolicies();
        services.AddSecurity();
        services.AddServices();

        services.AddScoped<EmployeeCreateEventConsumer>();

        services.AddHostedService<Test>();

        return services;
    }
}