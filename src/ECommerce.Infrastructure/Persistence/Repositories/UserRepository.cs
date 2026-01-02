using ECommerce.Domain.Users.Entities;
using ECommerce.Domain.Users.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public UserRepository(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<AppIdentityUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<AppIdentityUser?> GetByEmailAsync(string email,CancellationToken cancellationToken)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<AppIdentityUser?> GetByIdAsync(string id,CancellationToken cancellationToken)
    {
        return await _userManager.FindByIdAsync(id);
    }
}