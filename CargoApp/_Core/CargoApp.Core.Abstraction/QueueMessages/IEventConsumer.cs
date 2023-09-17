namespace CargoApp.Core.Abstraction.QueueMessages;

public interface IEventConsumer<in T>
{
    Task ProcessEvent(T @event);
}