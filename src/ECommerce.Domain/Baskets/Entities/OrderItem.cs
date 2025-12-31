using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Baskets.Entities;

public class OrderItem : Entity
{
    public string OrderId { get; private set; }
    public Order Order { get; private set; } 
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }

    private OrderItem() { }

    public static OrderItem Create(
        string productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        Guard.AgainstNullOrEmpty(productId, nameof(productId));
        Guard.AgainstNullOrEmpty(productName, nameof(productName));
        Guard.AgainstNegativeOrZero(unitPrice, nameof(unitPrice));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        var orderItem = new OrderItem
        {
            ProductId = productId,
            ProductName = productName,
            UnitPrice = unitPrice,
            Quantity = quantity,
            TotalPrice = unitPrice * quantity
        };

        return orderItem;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        Quantity = quantity;
        TotalPrice = UnitPrice * quantity;
    }
}