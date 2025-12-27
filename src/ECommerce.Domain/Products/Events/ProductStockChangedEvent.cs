using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Products.Events;

public record ProductStockChangedEvent(
    string ProductId,
    string ProductName,
    int OldStock,
    int NewStock
) : DomainEvent;