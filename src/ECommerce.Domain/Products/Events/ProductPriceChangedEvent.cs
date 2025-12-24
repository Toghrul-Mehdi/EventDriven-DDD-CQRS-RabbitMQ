using ECommerce.SharedKernel.Domain;
namespace ECommerce.Domain.Products.Events;
public record ProductPriceChangedEvent(
    string ProductId,
    string ProductName,
    decimal OldPrice,
    decimal NewPrice
) : DomainEvent;