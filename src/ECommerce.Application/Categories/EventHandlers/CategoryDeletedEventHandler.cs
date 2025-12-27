using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Categories.EventHandlers;

public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeletedEvent>
{
    private readonly ILogger<CategoryDeletedEventHandler> _logger;

    public CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Category deleted: ID={CategoryId}, Name={CategoryName}",
            notification.CategoryId,
            notification.CategoryName
        );

        return Task.CompletedTask;
    }
}