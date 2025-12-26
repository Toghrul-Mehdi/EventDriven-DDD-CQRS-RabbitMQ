using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ECommerce.Application.Products.EventHandlers;
public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Product created: {ProductId} - {ProductName} with price {Price}",
            notification.ProductId,
            notification.ProductName,
            notification.Price
        );

        return Task.CompletedTask;
    }
}