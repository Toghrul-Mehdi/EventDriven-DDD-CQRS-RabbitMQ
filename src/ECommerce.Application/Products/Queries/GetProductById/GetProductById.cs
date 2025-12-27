using ECommerce.Application.Products.Queries.GetProducts;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(string Id) : IRequest<Result<ProductDto>>;