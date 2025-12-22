using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Products.Queries.GetProducts;
public record GetProductsQuery() : IRequest<Result<List<ProductDto>>>;