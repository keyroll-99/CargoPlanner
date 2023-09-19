namespace CargoApp.Core.Abstraction.QueueMessages;

public interface IEventManager
{
    void PublishEvent<T>(T @event);

    void RegisterConsumer<T>(IEventConsumer<T> consumer) where T: class;
}