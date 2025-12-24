using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand(
    string Id,
    string Name,
    string Description
) : IRequest<Result<string>>;