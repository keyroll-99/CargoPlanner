using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IUserRepository, UserRepository>();
    }
}