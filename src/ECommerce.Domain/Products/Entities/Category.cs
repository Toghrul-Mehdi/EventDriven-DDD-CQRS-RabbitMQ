using ECommerce.Domain.Products.Events;
using ECommerce.SharedKernel.Domain;
namespace ECommerce.Domain.Products.Entities;
public class Category : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsDeleted { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Category() { }    
    public static Category Create(string name, string description)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        var category = new Category
        {
            Name = name,
            Description = description ?? string.Empty
        };

        category.AddDomainEvent(new CategoryCreatedEvent(
            category.Id,
            category.Name,
            category.Description
        ));

        return category;
    }    
    public void Update(string name, string description)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description ?? string.Empty;
    }    
    public bool HasProducts()
    {
        return _products.Any();
    }    
    public int GetProductCount()
    {
        return _products.Count;
    }
    public void Delete()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Category is already deleted.");

        IsDeleted = true;
    }
}