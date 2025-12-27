using ECommerce.Application.Categories.Commands.CreateCategory;
using ECommerce.Application.Categories.Commands.UpdateCategory;
using ECommerce.Application.Categories.Commands.DeleteCategory;
using ECommerce.Application.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Categories.Queries.GetCategoryById;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());

        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetCategoryById(id));

        if (result.IsFailure)
            return NotFound(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateCategoryCommand command)
    {
        var updatedCommand = command with { Id = id };

        var result = await _mediator.Send(updatedCommand);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));

        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return NoContent();
    }
}
