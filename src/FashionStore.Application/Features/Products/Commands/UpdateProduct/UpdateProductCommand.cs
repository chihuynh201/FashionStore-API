using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using Mapster;
using System.Text.Json.Serialization;

namespace FashionStore.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand : ICommand
{
    [JsonIgnore]
    public int ProductId { get; set; }
    public string ProductName { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int? FileId { get; init; }
    public int CategoryId { get; init; }
    public bool IsEnabled { get; init; }
}

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (product == null || product.IsDeleted)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Product not found.");
        }

        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Category not found.");
        }

        request.Adapt(product);

        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
