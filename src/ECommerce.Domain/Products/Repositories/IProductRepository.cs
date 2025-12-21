using ECommerce.Domain.Products.Entities;
namespace ECommerce.Domain.Products.Repositories;
public interface IProductRepository
{    
    Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken = default);    
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);    
    Task<List<Product>> GetByCategoryIdAsync(string categoryId, CancellationToken cancellationToken = default);    
    Task<List<Product>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);    
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);    
    Task AddAsync(Product product, CancellationToken cancellationToken = default);    
    void Update(Product product);    
    void Delete(Product product);    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}