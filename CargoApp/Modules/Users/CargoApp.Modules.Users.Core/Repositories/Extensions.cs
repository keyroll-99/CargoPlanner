﻿using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPasswordRecoveryRepository, PasswordRecoveryRepository>();
        return services;
    }
}