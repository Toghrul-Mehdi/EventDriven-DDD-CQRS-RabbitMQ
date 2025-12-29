using ECommerce.Application.Common.Messaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ECommerce.Infrastructure.MessageBroker.RabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    private readonly IConnection _connection;
    private readonly ILogger<RabbitMQEventBus> _logger;
    private const string ExchangeName = "ecommerce.events";

    public RabbitMQEventBus(
        IConnection connection,
        ILogger<RabbitMQEventBus> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    private async Task DeclareExchangeAsync()
    {
        await using var channel = await _connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: ExchangeName,
            type: ExchangeType.Topic,
            durable: true);
    }

    public async Task PublishAsync<T>(
        T @event,
        string routingKey = "",
        CancellationToken cancellationToken = default)
        where T : class
    {
        await DeclareExchangeAsync();

        await using var channel = await _connection.CreateChannelAsync();

        var eventName = @event.GetType().Name;
        routingKey = string.IsNullOrWhiteSpace(routingKey)
            ? eventName.ToLowerInvariant()
            : routingKey;

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json",
            Type = eventName
        };

        await channel.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);

        _logger.LogInformation(
            "Published event {EventName} with routing key {RoutingKey}",
            eventName,
            routingKey);
    }
}
