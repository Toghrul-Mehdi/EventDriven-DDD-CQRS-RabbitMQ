using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Products.Events;

public record ProductUpdatedEvent(
    string ProductId,
    string ProductName,
    string Description
) : DomainEvent;