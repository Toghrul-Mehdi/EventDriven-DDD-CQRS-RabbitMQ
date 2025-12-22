using ECommerce.Domain.Products.Entities;
using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
        if (!categoryExists)
        {
            return Result<string>.Failure("Category not found");
        }

        var product = Product.Create(
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.CategoryId
        );

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(product.Id);
    }
}