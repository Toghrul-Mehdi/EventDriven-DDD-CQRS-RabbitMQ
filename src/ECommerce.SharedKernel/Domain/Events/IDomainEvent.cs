using MediatR;
namespace ECommerce.SharedKernel.Domain;
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
    Guid EventId { get; }
}