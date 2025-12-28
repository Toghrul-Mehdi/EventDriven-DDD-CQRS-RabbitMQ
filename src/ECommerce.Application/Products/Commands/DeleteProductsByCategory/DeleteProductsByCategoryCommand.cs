using ECommerce.SharedKernel.Domain;
using MediatR;

namespace ECommerce.Application.Products.Commands.DeleteProductsByCategory;

public  record DeleteProductsByCategoryCommand(string CategoryId)
    : IRequest<Result>;

