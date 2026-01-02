using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Users.Entities;
using ECommerce.Infrastructure.MessageBroker.RabbitMQ;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Repositories;
using ECommerce.Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SQlServer")));

        services.AddIdentity<AppIdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Scan(scan => scan
            .FromAssemblyOf<ProductRepository>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());


        services.AddScoped<IAuthService, AuthService>();

        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));


        return services;
    }
}