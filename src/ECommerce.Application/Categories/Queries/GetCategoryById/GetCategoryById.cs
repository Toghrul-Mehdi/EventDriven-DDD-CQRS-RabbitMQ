using ECommerce.Application.Categories.Queries.GetCategories;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Categories.Queries.GetCategoryById;
public record GetCategoryById (string Id) : IRequest<Result<CategoryDto>>;


