using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Products.Commands.DeleteProductsByCategory;

public class DeleteProductsByCategoryCommandHandler
    : IRequestHandler<DeleteProductsByCategoryCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductsByCategoryCommandHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(
        DeleteProductsByCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository
            .GetByCategoryIdAsync(request.CategoryId, cancellationToken);

        if (products == null || !products.Any())
        {
            return Result.NotFound("No products found for this category");
        }

        foreach (var product in products)
        {
            if (product.IsDeleted)
                continue;

            product.Delete();              
            _productRepository.Update(product);
        }

        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result.NoContent("Products deleted successfully");
    }
}
