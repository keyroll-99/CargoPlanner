﻿using CargoApp.Core.Abstraction.Auth;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAuthManager, AuthManager>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}