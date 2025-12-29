namespace ECommerce.Infrastructure.MessageBroker.RabbitMQ;

public sealed class RabbitMQSettings
{
    // Connection
    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 5672;
    public string Username { get; init; } = "guest";
    public string Password { get; init; } = "guest";
    public string VirtualHost { get; init; } = "/";

    // Exchange
    public string ExchangeName { get; init; } = "ecommerce.events";
    public string ExchangeType { get; init; } = "topic";
    public bool ExchangeDurable { get; init; } = true;
    public bool ExchangeAutoDelete { get; init; } = false;

    // Queue
    public string QueueName { get; init; } = "ecommerce.queue";
    public bool QueueDurable { get; init; } = true;
    public bool QueueExclusive { get; init; } = false;
    public bool QueueAutoDelete { get; init; } = false;

    // Routing
    public string RoutingKey { get; init; } = "#";

    // Reliability / Resilience
    public bool EnablePublisherConfirms { get; init; } = true;
    public int RetryCount { get; init; } = 3;
    public int RetryDelaySeconds { get; init; } = 5;

    // Dead Letter
    public string DeadLetterExchange { get; init; } = "ecommerce.dlx";
    public string DeadLetterRoutingKey { get; init; } = "dead-letter";
}
