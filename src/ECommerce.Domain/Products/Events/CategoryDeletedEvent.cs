using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Products.Events;

public record CategoryDeletedEvent(
    string CategoryId,
    string CategoryName
) : DomainEvent;