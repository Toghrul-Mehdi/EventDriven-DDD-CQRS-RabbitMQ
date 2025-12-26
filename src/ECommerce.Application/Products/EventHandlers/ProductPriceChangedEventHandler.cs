using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ECommerce.Application.Products.EventHandlers;
public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
{
    private readonly ILogger<ProductPriceChangedEventHandler> _logger;

    public ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Product price changed: {ProductId} - {ProductName} from {OldPrice} to {NewPrice}",
            notification.ProductId,
            notification.ProductName,
            notification.OldPrice,
            notification.NewPrice
        );

        return Task.CompletedTask;
    }
}