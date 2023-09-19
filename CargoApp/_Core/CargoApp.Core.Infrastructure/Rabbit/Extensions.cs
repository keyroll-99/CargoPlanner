using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Rabbit.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Rabbit;

public static class Extensions
{
    private const string OptionsName = "Rabbit";

    public static IServiceCollection AddRabbit(this IServiceCollection services)
    {
        var options = services.GetOptions<RabbitOptions>(OptionsName);

        services.AddSingleton<RabbitFactory>(_ => new RabbitFactory(options.HostName));
        services.AddSingleton<IEventManager, RabbitEventManager>();
        
        return services;
    }
}