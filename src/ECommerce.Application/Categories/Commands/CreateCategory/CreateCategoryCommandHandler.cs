using ECommerce.Domain.Products.Entities;
using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<string>>
{
    private readonly ICategoryRepository _categoryRepository;
    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var exsistCategory = await _categoryRepository.ExistsAsync(request.Name,cancellationToken);
        if (exsistCategory)
        {
            return Result<string>.Failure("Category name already exsist.");
        }

        var category = Category.Create(
            request.Name,
            request.Description
        );

        await _categoryRepository.AddAsync(category,cancellationToken);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(category.Id);
    }
}
