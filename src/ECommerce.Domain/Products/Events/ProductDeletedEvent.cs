using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Products.Events;

public record ProductDeletedEvent(
    string ProductId,
    string ProductName
) : DomainEvent;