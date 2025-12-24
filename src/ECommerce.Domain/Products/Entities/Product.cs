using ECommerce.SharedKernel.Domain;
using ECommerce.Domain.Products.Events;

namespace ECommerce.Domain.Products.Entities;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string CategoryId { get; private set; }
    public Category Category { get; private set; }
    public bool IsDeleted { get; private set; }

    private Product() { }

    public static Product Create(
        string name,
        string description,
        decimal price,
        int stock,
        string categoryId)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstNullOrEmpty(categoryId, nameof(categoryId));
        Guard.AgainstNegativeOrZero(price, nameof(price));

        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative", nameof(stock));

        var product = new Product
        {
            Name = name,
            Description = description ?? string.Empty,
            Price = price,
            Stock = stock,
            CategoryId = categoryId,
            IsDeleted = false
        };

        product.AddDomainEvent(new ProductCreatedEvent(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.CategoryId
        ));

        return product;
    }

    public void UpdatePrice(decimal newPrice)
    {
        Guard.AgainstNegativeOrZero(newPrice, nameof(newPrice));

        var oldPrice = Price;
        Price = newPrice;

        AddDomainEvent(new ProductPriceChangedEvent(
            Id,
            Name,
            oldPrice,
            newPrice
        ));
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        Stock += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        if (Stock < quantity)
            throw new InvalidOperationException("Insufficient stock");

        Stock -= quantity;
    }

    public void Update(string name, string description)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description ?? string.Empty;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}