using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.DTOs.User;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<string>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.AcceptTerms)
            return Result<string>.Failure("You must accept the terms and conditions.");

        if (request.Password != request.ConfirmPassword)
            return Result<string>.Failure("Passwords do not match.");

        var dto = new RegisterUserDto
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            DateBirth = request.DateBirth,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            AcceptTerms = request.AcceptTerms
        };

        var result = await _authService.RegisterAsync(dto, cancellationToken);

        return result;
    }
}
