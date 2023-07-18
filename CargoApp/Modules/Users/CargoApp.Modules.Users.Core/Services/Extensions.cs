using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddSingleton<IAuthService, AuthService>();
}