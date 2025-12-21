using ECommerce.SharedKernel.Domain;
namespace ECommerce.Domain.Products.Entities;
public class Category : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Category() { }    
    public static Category Create(string name, string description)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        return new Category
        {
            Name = name,
            Description = description ?? string.Empty
        };
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
}