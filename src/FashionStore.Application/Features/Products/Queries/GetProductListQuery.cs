using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.QueryModels;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.Products.Queries;

public record GetProductListQuery(ProductQueryModel QueryModel) : IRequest<ActionResponse<PagedResponse<ProductDto>>>;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ActionResponse<PagedResponse<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<PagedResponse<ProductDto>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ProductRepository.GetPagedAsync(request.QueryModel);
        return ActionResponse<PagedResponse<ProductDto>>.Success(result);
    }
}
