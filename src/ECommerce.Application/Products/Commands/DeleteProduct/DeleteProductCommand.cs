using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Products.Commands.DeleteProduct;
public record DeleteProductCommand(    
    string ProductId
) : IRequest<Result<string>>;

