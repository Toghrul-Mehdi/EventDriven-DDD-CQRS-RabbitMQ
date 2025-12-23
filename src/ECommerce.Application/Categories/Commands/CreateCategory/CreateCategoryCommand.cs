using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Categories.Commands.CreateCategory;
public record CreateCategoryCommand(
    string Name,
    string Description
    ) : IRequest<Result<string>>;

