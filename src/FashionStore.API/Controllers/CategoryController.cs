using FashionStore.API.Extensions;
using FashionStore.Application.Features.Categories.Commands.CreateCategory;
using FashionStore.Application.Features.Categories.Commands.DeleteCategory;
using FashionStore.Application.Features.Categories.Commands.UpdateCategory;
using FashionStore.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Controllers;

[ApiController]
[Authorize]
public class CategoryController : StoreBaseController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] string search = null)
    {
        var result = await _mediator.Send(new GetCategoryListQuery(search));
        return result.AsObjectResult();
    }

    [HttpGet("{categoryId:int:min(1)}")]
    public async Task<IActionResult> GetCategory(int categoryId)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
        return result.AsObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

    [HttpPut("{categoryId:int:min(1)}")]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryCommand command)
    {
        command.CategoryId = categoryId;
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

    [HttpDelete("{categoryId:int:min(1)}")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(categoryId));
        return result.AsObjectResult();
    }
}
