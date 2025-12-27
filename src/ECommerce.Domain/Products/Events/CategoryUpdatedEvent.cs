using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Products.Events;

public record CategoryUpdatedEvent(
    string CategoryId,
    string CategoryName,
    string Description
) : DomainEvent;