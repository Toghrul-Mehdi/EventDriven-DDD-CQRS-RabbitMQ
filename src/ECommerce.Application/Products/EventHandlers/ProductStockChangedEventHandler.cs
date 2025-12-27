using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Products.EventHandlers;

public class ProductStockChangedEventHandler : INotificationHandler<ProductStockChangedEvent>
{
    private readonly ILogger<ProductStockChangedEventHandler> _logger;

    public ProductStockChangedEventHandler(ILogger<ProductStockChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductStockChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Product stock changed: ID={ProductId}, Name={ProductName}, OldStock={OldStock}, NewStock={NewStock}",
            notification.ProductId,
            notification.ProductName,
            notification.OldStock,
            notification.NewStock
        );

        return Task.CompletedTask;
    }
}