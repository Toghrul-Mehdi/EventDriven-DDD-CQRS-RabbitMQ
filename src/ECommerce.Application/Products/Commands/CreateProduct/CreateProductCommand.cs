using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string CategoryId
) : IRequest<Result<string>>;