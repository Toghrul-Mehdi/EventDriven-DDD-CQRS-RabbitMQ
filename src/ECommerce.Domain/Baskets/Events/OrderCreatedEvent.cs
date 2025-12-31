using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Baskets.Events;

public record OrderCreatedEvent(
    string OrderId,
    string UserId,
    decimal TotalAmount,
    List<OrderItemDto> Items
) : DomainEvent;

public record OrderItemDto(
    string ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);