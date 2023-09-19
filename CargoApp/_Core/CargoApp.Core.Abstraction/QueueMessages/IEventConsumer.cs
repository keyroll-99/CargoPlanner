namespace CargoApp.Core.Abstraction.QueueMessages;

public interface IEventConsumer<in T> where T: class
{
    Task Process(T @event);
}