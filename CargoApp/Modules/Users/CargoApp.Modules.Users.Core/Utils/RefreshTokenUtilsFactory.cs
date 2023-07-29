using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Users.Core.Utils;

internal class RefreshTokenUtilsFactory : IRefreshTokenUtilsFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RefreshTokenUtilsFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IRefreshTokenUtils Create()
        => _serviceProvider.GetRequiredService<IRefreshTokenUtils>();
}