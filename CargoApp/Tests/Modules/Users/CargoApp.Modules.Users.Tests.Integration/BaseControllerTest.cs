using System.Net.Http.Headers;
using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Infrastructure.Auth;
using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Users.Core.Security;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Integration;

[Collection("Api")]
public abstract class BaseControllerTest : IClassFixture<OptionsProvider>
{
    private readonly AuthManager _authManager;
    internal readonly HttpClient Client;

    protected JsonWebToken Authorize(Guid userId, string email, PermissionEnum permissionEnum, Guid? companyId = null)
    {
        var jwt = _authManager.CreateToken(userId, email, permissionEnum, companyId ?? Guid.Empty);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        return jwt;
    }
    
    
    public BaseControllerTest(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("jwt");
        _authManager = new AuthManager(new Clock(), authOptions);
        
        var app = new CargoAppTest(ConfigureServices);
        Client = app.Client;
    }

    protected virtual void ConfigureServices(IServiceCollection services){}
}