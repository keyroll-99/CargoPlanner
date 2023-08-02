using CargoApp.Modules.Contracts.Users;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using CargoApp.Modules.Users.Core.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUser, UserService>()
            .AddScoped<IRefreshTokenService, RefreshTokenService>();
    }
}