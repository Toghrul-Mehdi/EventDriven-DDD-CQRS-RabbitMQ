using ECommerce.SharedKernel.Domain;
namespace ECommerce.Domain.Products.Events;
public record ProductCreatedEvent(
    string ProductId,
    string ProductName,
    string Description,
    decimal Price,
    int Stock,
    string CategoryId
) : DomainEvent;