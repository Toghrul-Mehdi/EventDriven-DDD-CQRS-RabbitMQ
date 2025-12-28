using ECommerce.Domain.Products.Repositories;
using ECommerce.SharedKernel.Domain;
using MediatR;
namespace ECommerce.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository;
    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetWithProductsAsync(request.CategoryId, cancellationToken);

        if (category == null)
        {
            return Result.NotFound("Category not found");
        }

        if (category.HasProducts())
        {
            return Result.Conflict("Cannot delete category with existing products");
        }

        category.Delete();
        _categoryRepository.Delete(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return Result.NoContent("Category deleted successfully");
    }
}
