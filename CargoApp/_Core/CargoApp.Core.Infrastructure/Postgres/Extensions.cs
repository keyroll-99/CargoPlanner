using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;

namespace CargoApp.Core.Infrastructure.Postgres;

public static class Extensions
{
    private const string SectionName = "Postgres";

    internal static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        var options = services.GetOptions<PostgresOptions>(SectionName);
        services.AddSingleton(options);
        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<PostgresOptions>(SectionName);
        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer<T>>();
        services.AddSingleton<IServiceProvider, ServiceProvider>();
        return services;
    }
}