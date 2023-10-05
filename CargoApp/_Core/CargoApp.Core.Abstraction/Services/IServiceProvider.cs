namespace CargoApp.Core.Abstraction.Services;

public interface IServiceProvider
{
    public Task<TService> GetService<TService>();
}