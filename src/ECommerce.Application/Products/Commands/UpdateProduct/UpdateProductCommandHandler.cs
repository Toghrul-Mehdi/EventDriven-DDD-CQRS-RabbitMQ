using ECommerce.Domain.Products.Entities;
using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository
            .GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Result<string>.Failure("Product not found");
        }

        var categoryExists = await _categoryRepository
            .ExistsAsync(request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            return Result<string>.Failure("Category not found");
        }
        product.Update(request.Name, request.Description);

        if (product.Price != request.Price)
        {
            product.UpdatePrice(request.Price);
        }

        if (request.Stock > product.Stock)
        {
            product.AddStock(request.Stock - product.Stock);
        }
        else if (request.Stock < product.Stock)
        {
            product.RemoveStock(product.Stock - request.Stock);
        }
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(product.Id);
    }
}
