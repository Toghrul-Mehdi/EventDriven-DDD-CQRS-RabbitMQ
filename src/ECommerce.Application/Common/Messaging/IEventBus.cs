namespace ECommerce.Application.Common.Messaging;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, string routingKey = "", CancellationToken cancellationToken = default)
        where T : class;
}
