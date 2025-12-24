namespace ECommerce.SharedKernel.Domain;
public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; init; }
    public Guid EventId { get; init; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
        EventId = Guid.NewGuid();
    }
}