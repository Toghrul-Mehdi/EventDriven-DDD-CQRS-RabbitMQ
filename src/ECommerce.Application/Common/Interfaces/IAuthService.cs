using ECommerce.Application.DTOs.User;
using ECommerce.Application.Users.Commands.RegisterUser;
using ECommerce.SharedKernel.Domain;

namespace ECommerce.Application.Common.Interfaces;
public interface IAuthService
{
    Task<Result<string>> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken);
}
