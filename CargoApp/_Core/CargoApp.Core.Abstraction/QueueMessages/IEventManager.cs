namespace CargoApp.Core.Abstraction.QueueMessages;

public interface IEventManager
{
    void PublishEvent<T>(T @event);
}