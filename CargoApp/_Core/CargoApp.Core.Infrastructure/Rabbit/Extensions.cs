using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Rabbit.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CargoApp.Core.Infrastructure.Rabbit;

public static class Extensions
{
    private const string OptionsName = "Rabbit";

    public static IServiceCollection AddRabbit(this IServiceCollection services)
    {
        var options = services.GetOptions<RabbitOptions>(OptionsName);

        services.AddSingleton<IEventManager>(sp =>
            RabbitFactory.CreateEventManager(options.HostName, sp.GetRequiredService<ILogger>()));

        return services;
    }

    public static IServiceCollection AddEventConsumer<TProcessor, TEvent>(this IServiceCollection services)
    where TEvent: class
    where TProcessor: IEventConsumer<TEvent>
    {
        services.Add(new ServiceDescriptor(
            typeof(TProcessor),
            typeof(TProcessor),
            ServiceLifetime.Singleton
        ));

        services.AddHostedService<RabbitEventConsumer<TProcessor, TEvent>>();
        return services;
    }
}