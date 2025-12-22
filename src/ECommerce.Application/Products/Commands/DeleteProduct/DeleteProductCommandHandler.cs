using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("Product not found.");
        }
        product.Delete();
        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Product delete succesfully.");
    }
}
