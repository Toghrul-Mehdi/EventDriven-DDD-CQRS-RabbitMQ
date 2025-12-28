using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
        {
            return Result.NotFound("Product not found");
        }

        product.Delete();
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result.NoContent("Product deleted successfully");
    }
}
