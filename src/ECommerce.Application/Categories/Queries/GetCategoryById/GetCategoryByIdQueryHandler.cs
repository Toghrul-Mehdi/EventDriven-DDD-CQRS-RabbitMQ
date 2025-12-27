using ECommerce.Application.Categories.Queries.GetCategories;
using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryById, Result<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryDto>> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
            return Result<CategoryDto>.Failure("Category not found");

        var dto = new CategoryDto
        {
            CategoryId=category.Id,
            Name=category.Name,
            Description=category.Description
        };

        return Result<CategoryDto>.Success(dto);
    }
}

