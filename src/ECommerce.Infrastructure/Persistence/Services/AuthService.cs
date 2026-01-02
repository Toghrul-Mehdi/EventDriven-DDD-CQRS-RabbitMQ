using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.DTOs.User;
using ECommerce.Domain.Users.Entities;
using ECommerce.SharedKernel.Domain;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Persistence.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public AuthService(
        UserManager<AppIdentityUser> userManager,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<Result<string>> RegisterAsync(
        RegisterUserDto dto,
        CancellationToken cancellationToken)
    {
        var existingIdentityUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingIdentityUser is not null)
            return Result<string>.Failure("User already exists.");

        var identityUser = new AppIdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            Name = dto.Name,
            Surname = dto.Surname,
            DateBirth = dto.DateBirth
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var identityResult = await _userManager.CreateAsync(identityUser, dto.Password);

        if (!identityResult.Succeeded)
        {
            var error = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            return Result<string>.Failure(error);
        }

        var domainUser = new User(
            id: identityUser.Id, 
            name: dto.Name,
            surname: dto.Surname,
            email: dto.Email,
            dateBirth: dto.DateBirth
        );

        _dbContext.Users.Add(domainUser);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return Result<string>.Success(identityUser.Id,"User Registered Successfully.");
    }
}
