using ECommerce.Domain.Users.Entities;

namespace ECommerce.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<AppIdentityUser?> GetByIdAsync(string id,CancellationToken cancellationToken);
    Task<AppIdentityUser?> GetByEmailAsync(string email,CancellationToken cancellationToken);
    Task<List<AppIdentityUser>> GetAllAsync(CancellationToken cancellationToken);
}
