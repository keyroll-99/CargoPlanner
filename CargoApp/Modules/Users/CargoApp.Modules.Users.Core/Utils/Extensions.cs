using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Utils;

public static class Extensions
{
    public static IServiceCollection AddUtils(this IServiceCollection services)
    {
        services.AddSingleton<IRefreshTokenUtilsFactory, RefreshTokenUtilsFactory>();
        services.AddScoped<IRefreshTokenUtils>(sp => sp.GetRequiredService<IRefreshTokenUtilsFactory>().Create());
        return services;
    }
}