using ECommerce.SharedKernel.Domain;
namespace ECommerce.Domain.Products.Events;
public record CategoryCreatedEvent(
    string CategoryId,
    string CategoryName,
    string CategoryDescription
) : DomainEvent;