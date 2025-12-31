using ECommerce.Domain.Baskets.Enums;
using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Baskets.Events;

public record OrderStatusChangedEvent(
    string OrderId,
    OrderStatus OldStatus,
    OrderStatus NewStatus
) : DomainEvent;