using CargoApp.Modules.Contracts.User;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUser, UserService>();
    }
}