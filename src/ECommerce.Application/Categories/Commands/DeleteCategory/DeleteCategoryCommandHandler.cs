using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    private readonly ICategoryRepository _categoryRepository;
    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result<string>.Failure("Category not found.");
        }
        category.Delete();
        _categoryRepository.Delete(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Category delete succesfully.");
    }
}
