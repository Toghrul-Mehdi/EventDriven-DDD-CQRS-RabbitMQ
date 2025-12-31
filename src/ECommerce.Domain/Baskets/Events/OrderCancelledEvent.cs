using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Baskets.Events;

public record OrderCancelledEvent(
    string OrderId,
    string UserId,
    decimal TotalAmount
) : DomainEvent;