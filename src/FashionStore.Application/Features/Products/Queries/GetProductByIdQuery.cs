using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.Products.Queries;

public record GetProductByIdQuery(int ProductId) : IRequest<ActionResponse<ProductDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ActionResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(request.ProductId);
        if (product == null)
        {
            return ActionResponse<ProductDto>.CreateResponse(ResponseCode.NotFound, message: "Product not found.");
        }

        return ActionResponse<ProductDto>.Success(product);
    }
}
