using ECommerce.Application.Products.Commands.CreateProduct;
using ECommerce.Application.Products.Commands.UpdateProduct;
using ECommerce.Application.Products.Commands.DeleteProduct;
using ECommerce.Application.Products.Queries.GetProducts;
using ECommerce.Application.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Products.Commands.DeleteProductsByCategory;

namespace ECommerce.API.Controllers;

[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductDto dto)
    {
        var command = new UpdateProductCommand(
            id, dto.Name, dto.Description, dto.Price, dto.Stock, dto.CategoryId);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return HandleResult(result);
    }
    [HttpDelete("by-category/{categoryId}")]
    public async Task<IActionResult> DeleteByCategory(string categoryId)
    {
        var result = await _mediator.Send(new DeleteProductsByCategoryCommand(categoryId));
        return HandleResult(result);
    }

}

