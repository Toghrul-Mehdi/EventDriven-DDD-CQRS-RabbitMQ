using ECommerce.Domain.Products.Entities;
using ECommerce.Domain.Products.Repositories;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.Infrastructure.Persistence.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Category?> GetWithProductsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {   
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}