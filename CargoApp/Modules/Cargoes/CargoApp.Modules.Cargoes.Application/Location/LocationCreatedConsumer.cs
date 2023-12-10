using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using CargoApp.Modules.Contracts.Events.Locations;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;


namespace CargoApp.Modules.Cargoes.Application.Location;

public class LocationCreatedConsumer : IEventConsumer<LocationCreatedEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public LocationCreatedConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task Process(LocationCreatedEvent createdEvent)
    {
        var repository = await _serviceProvider.GetService<ILocationRepository>();

        await repository.AddAsync(new Core.LocationAggregate.Location(createdEvent.Lat, createdEvent.Lon, createdEvent.Name, createdEvent.OsmId));
    }
}