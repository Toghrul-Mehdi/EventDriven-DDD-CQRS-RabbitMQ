using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Categories.Queries.GetCategories;
public record GetCategoriesQuery() : IRequest<Result<List<CategoryDto>>>;

