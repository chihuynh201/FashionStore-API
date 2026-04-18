using FashionStore.API.Extensions;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Features.AttributeValues.Commands.UpdateAttributeValue;
using FashionStore.Application.Features.AttributeValues.Queries;
using FashionStore.Application.Features.ProductAttributes.Commands.CreateAttribute;
using FashionStore.Application.Features.ProductAttributes.Commands.DeleteAttribute;
using FashionStore.Application.Features.ProductAttributes.Commands.UpdateAttribute;
using FashionStore.Application.Features.ProductAttributes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Controllers;

[ApiController]
[Authorize]
public class ProductAttributeController : StoreBaseController
{
    private readonly IMediator _mediator;

    public ProductAttributeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttributes([FromQuery] QueryModel queryModel)
    {
        var result = await _mediator.Send(new GetProductAttributeListQuery(queryModel));
        return result.AsObjectResult();
    }

    [HttpGet("{attributeId:int:min(1)}")]
    public async Task<IActionResult> GetAttribute(int attributeId)
    {
        var result = await _mediator.Send(new GetProductAttributeByIdQuery(attributeId));
        return result.AsObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttribute([FromBody] CreateProductAttributeCommand command)
    {
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

    [HttpPut("{attributeId:int:min(1)}")]
    public async Task<IActionResult> UpdateAttribute(int attributeId, [FromBody] UpdateProductAttributeCommand command)
    {
        command.ProductAttributeId = attributeId;
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

    [HttpDelete("{attributeId:int:min(1)}")]
    public async Task<IActionResult> DeleteAttribute(int attributeId)
    {
        var result = await _mediator.Send(new DeleteProductAttributeCommand(attributeId));
        return result.AsObjectResult();
    }

    [HttpGet("{attributeId:int:min(1)}/values")]
    public async Task<IActionResult> GetAttributeValues(int attributeId)
    {
        var result = await _mediator.Send(new GetAttributeValueListQuery(attributeId));
        return result.AsObjectResult();
    }

    [HttpPut("{attributeId:int:min(1)}/values")]
    public async Task<IActionResult> UpdateListAttributeValues(int attributeId, [FromBody] UpdateAttributeValueCommand command)
    {
        command.ProductAttributeId = attributeId;
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

}
