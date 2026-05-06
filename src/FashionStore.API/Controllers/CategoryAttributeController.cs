using FashionStore.API.Extensions;
using FashionStore.Application.Features.CategoryAttributes.Commands;
using FashionStore.Application.Features.CategoryAttributes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoryAttributeController : StoreBaseController
{
    private readonly IMediator _mediator;

    public CategoryAttributeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoryWithAttributes([FromQuery] string search = null)
    {
        var result = await _mediator.Send(new GetAllCategoryAttributesQuery(search));
        return result.AsObjectResult();
    }

    [HttpGet("{categoryId:int:min(1)}")]
    public async Task<IActionResult> GetAttributes(int categoryId)
    {
        var result = await _mediator.Send(new GetAttributesByCategoryQuery(categoryId));
        return result.AsObjectResult();
    }

    [HttpPost("{categoryId:int:min(1)}")]
    public async Task<IActionResult> AssignAttributes(int categoryId, [FromBody] List<int> attributeIds)
    {
        var result = await _mediator.Send(new AssignCategoryAttributesCommand(categoryId, attributeIds));
        return result.AsObjectResult();
    }
}
