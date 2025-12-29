using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace ECommerce.Infrastructure.MessageBroker.RabbitMQ;

public sealed class RabbitMQConnection : IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly ILogger<RabbitMQConnection> _logger;

    private RabbitMQConnection(
        IConnection connection,
        ILogger<RabbitMQConnection> logger)
    {
        _connection = connection;
        _logger = logger;

        _logger.LogInformation("RabbitMQ connection established");
    }

    public static async Task<RabbitMQConnection> CreateAsync(
        IConfiguration configuration,
        ILogger<RabbitMQConnection> logger)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"] ?? "localhost",
            Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
            UserName = configuration["RabbitMQ:Username"] ?? "guest",
            Password = configuration["RabbitMQ:Password"] ?? "guest"
        };


        var connection = await factory.CreateConnectionAsync();

        return new RabbitMQConnection(connection, logger);
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        return await _connection.CreateChannelAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.CloseAsync();
        _connection.Dispose();
    }
}
