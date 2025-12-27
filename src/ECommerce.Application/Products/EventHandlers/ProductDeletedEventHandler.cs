using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Products.EventHandlers;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Product deleted: ID={ProductId}, Name={ProductName}",
            notification.ProductId,
            notification.ProductName
        );

        return Task.CompletedTask;
    }
}