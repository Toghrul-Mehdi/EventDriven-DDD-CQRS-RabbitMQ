using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Categories.Commands.DeleteCategory;
public record DeleteCategoryCommand(
    string CategoryId
    ) : IRequest<Result>;

