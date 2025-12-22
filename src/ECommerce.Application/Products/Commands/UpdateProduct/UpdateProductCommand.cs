using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Products.Commands.UpdateProduct;
public record UpdateProductCommand(
    string ProductId,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string CategoryId
) : IRequest<Result<string>>;
