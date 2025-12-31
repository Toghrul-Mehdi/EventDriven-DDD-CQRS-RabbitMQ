using ECommerce.Domain.Baskets.Enums;
using ECommerce.Domain.Baskets.Events;
using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Baskets.Entities;

public class Order : Entity
{
    public string UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string ShippingAddress { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public bool IsDeleted { get; private set; }

    // ← readonly-u SİL
    private List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order() { }

    public static Order Create(
        string userId,
        string shippingAddress,
        List<OrderItem> orderItems)
    {
        Guard.AgainstNullOrEmpty(userId, nameof(userId));
        Guard.AgainstNullOrEmpty(shippingAddress, nameof(shippingAddress));

        if (orderItems == null || !orderItems.Any())
            throw new ArgumentException("Order must have at least one item", nameof(orderItems));

        var order = new Order
        {
            UserId = userId,
            Status = OrderStatus.Pending,
            ShippingAddress = shippingAddress,
            OrderDate = DateTime.UtcNow,
            _orderItems = orderItems,  // ← İndi işləyəcək
            IsDeleted = false
        };

        order.CalculateTotalAmount();

        order.AddDomainEvent(new OrderCreatedEvent(
            order.Id,
            order.UserId,
            order.TotalAmount,
            order.OrderItems.Select(i => new OrderItemDto(
                i.ProductId,
                i.ProductName,
                i.Quantity,
                i.UnitPrice
            )).ToList()
        ));

        return order;
    }

    public void ConfirmOrder()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be confirmed");

        var oldStatus = Status;
        Status = OrderStatus.Confirmed;

        AddDomainEvent(new OrderStatusChangedEvent(Id, oldStatus, Status));
    }

    public void StartProcessing()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed orders can be processed");

        var oldStatus = Status;
        Status = OrderStatus.Processing;

        AddDomainEvent(new OrderStatusChangedEvent(Id, oldStatus, Status));
    }

    public void ShipOrder()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Only processing orders can be shipped");

        var oldStatus = Status;
        Status = OrderStatus.Shipped;

        AddDomainEvent(new OrderStatusChangedEvent(Id, oldStatus, Status));
    }

    public void DeliverOrder()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Only shipped orders can be delivered");

        var oldStatus = Status;
        Status = OrderStatus.Delivered;
        DeliveredDate = DateTime.UtcNow;

        AddDomainEvent(new OrderStatusChangedEvent(Id, oldStatus, Status));
    }

    public void CancelOrder()
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Delivered orders cannot be cancelled");

        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled");

        var oldStatus = Status;
        Status = OrderStatus.Cancelled;

        AddDomainEvent(new OrderCancelledEvent(Id, UserId, TotalAmount));
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot add items to non-pending orders");

        _orderItems.Add(item);
        CalculateTotalAmount();
    }

    public void RemoveItem(string orderItemId)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot remove items from non-pending orders");

        var item = _orderItems.FirstOrDefault(i => i.Id == orderItemId);
        if (item == null)
            throw new InvalidOperationException("Order item not found");

        _orderItems.Remove(item);
        CalculateTotalAmount();
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = _orderItems.Sum(i => i.TotalPrice);
    }

    public void Delete()
    {
        if (Status == OrderStatus.Processing || Status == OrderStatus.Shipped)
            throw new InvalidOperationException("Cannot delete orders in processing or shipped status");

        IsDeleted = true;
    }
}