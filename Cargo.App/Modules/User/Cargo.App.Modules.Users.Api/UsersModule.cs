using Cargo.App.Module.User.Core;
using Cargo.App.Shared.Abstraction.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cargo.App.Module.User.Api;

public sealed class UsersModule : IModule
{
    public string Name => "Users";
    public const string BasePath = "Users";
    public void AddModule(IServiceCollection services)
    {
        services.AddCore();
    }

    public void UseModule(IApplicationBuilder app)
    {
        throw new NotImplementedException();
    }
}