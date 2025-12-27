using ECommerce.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Categories.EventHandlers;

public class CategoryUpdatedEventHandler : INotificationHandler<CategoryUpdatedEvent>
{
    private readonly ILogger<CategoryUpdatedEventHandler> _logger;

    public CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Category updated: ID={CategoryId}, Name={CategoryName}",
            notification.CategoryId,
            notification.CategoryName
        );

        return Task.CompletedTask;
    }
}