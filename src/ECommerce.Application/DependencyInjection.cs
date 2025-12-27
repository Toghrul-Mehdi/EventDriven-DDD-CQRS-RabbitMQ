using ECommerce.Application.Products.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ProductCreatedEventHandler).Assembly));

        return services;
    }
}
