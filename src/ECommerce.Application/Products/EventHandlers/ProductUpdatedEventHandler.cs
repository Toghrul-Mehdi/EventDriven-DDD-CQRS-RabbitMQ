using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Products.EventHandlers;

public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;

    public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Product updated: ID={ProductId}, Name={ProductName}",
            notification.ProductId,
            notification.ProductName
        );

        return Task.CompletedTask;
    }
}