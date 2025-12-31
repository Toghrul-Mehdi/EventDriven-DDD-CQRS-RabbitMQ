using ECommerce.Domain.Baskets.Entities;
using ECommerce.Domain.Baskets.Enums;

namespace ECommerce.Domain.Baskets.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdWithItemsAsync(string id, CancellationToken cancellationToken = default);
    Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Order>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<List<Order>> GetByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    void Update(Order order);
    void Delete(Order order);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}