using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cargo.App.Shared.Abstraction.Module;

public interface IModule
{
    public string Name { get; }
    public void AddModule(IServiceCollection  services);
    public void UseModule(IApplicationBuilder app);
}