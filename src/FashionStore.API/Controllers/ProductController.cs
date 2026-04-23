using FashionStore.API.Extensions;
using FashionStore.Application.Common.DTOs.QueryModels;
using FashionStore.Application.Features.Products.Commands.CreateProduct;
using FashionStore.Application.Features.Products.Commands.DeleteProduct;
using FashionStore.Application.Features.Products.Commands.UpdateProduct;
using FashionStore.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductController : StoreBaseController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryModel queryModel)
    {
        var result = await _mediator.Send(new GetProductListQuery(queryModel));
        return result.AsObjectResult();
    }

    [HttpGet("{productId:int:min(1)}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(productId));
        return result.AsObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

    [HttpPut("{productId:int:min(1)}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductCommand command)
    {
        command.ProductId = productId;
        var result = await _mediator.Send(command);
        return result.AsObjectResult();
    }

    [HttpDelete("{productId:int:min(1)}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _mediator.Send(new DeleteProductCommand(productId));
        return result.AsObjectResult();
    }
}
