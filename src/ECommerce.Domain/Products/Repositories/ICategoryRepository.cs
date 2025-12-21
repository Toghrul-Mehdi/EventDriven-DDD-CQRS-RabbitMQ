using ECommerce.Domain.Products.Entities;
namespace ECommerce.Domain.Products.Repositories;
public interface ICategoryRepository
{    
    Task<Category?> GetByIdAsync(string id, CancellationToken cancellationToken = default);    
    Task<Category?> GetWithProductsAsync(string id, CancellationToken cancellationToken = default);    
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);    
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);    
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);    
    Task AddAsync(Category category, CancellationToken cancellationToken = default);    
    void Update(Category category);    
    void Delete(Category category);    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}