using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(
    string Name,
    string Surname,
    string Email,
    DateTime DateBirth,
    string Password,
    string ConfirmPassword,
    bool AcceptTerms) : IRequest<Result<string>>;

