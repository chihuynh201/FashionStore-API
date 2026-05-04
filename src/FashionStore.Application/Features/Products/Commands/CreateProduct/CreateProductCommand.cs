using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Mapster;

namespace FashionStore.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand : ICommand<int>
{
    public string ProductName { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int? FileId { get; init; }
    public int CategoryId { get; init; }
}

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            return ActionResponse<int>.CreateResponse(ResponseCode.NotFound, message: "Category not found.");
        }

        var product = request.Adapt<Product>();

        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse<int>.Success(product.ProductId);
    }
}
