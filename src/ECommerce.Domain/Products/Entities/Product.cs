using ECommerce.SharedKernel.Domain;
namespace ECommerce.Domain.Products.Entities;
public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string CategoryId { get; private set; }
    public Category Category { get; private set; }

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

        return new Product
        {
            Name = name,
            Description = description ?? string.Empty,
            Price = price,
            Stock = stock,
            CategoryId = categoryId
        };

    }

    public void UpdatePrice(decimal newPrice)
    {
        Guard.AgainstNegativeOrZero(newPrice, nameof(newPrice));
        Price = newPrice;        
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
}
