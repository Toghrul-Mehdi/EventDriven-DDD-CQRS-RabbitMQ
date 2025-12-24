using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<string>>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
        {
            return Result<string>.Failure("Category not found");
        }

        var exsistCategory = await _categoryRepository.ExistsAsync(request.Name, cancellationToken);
        if (exsistCategory)
        {
            return Result<string>.Failure("Category name already exsist.");
        }

        category.Update(request.Name, request.Description);

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Category updated succesfully.");
    }
}