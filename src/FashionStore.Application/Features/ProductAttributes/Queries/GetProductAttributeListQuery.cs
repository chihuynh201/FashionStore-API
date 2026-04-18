using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.ProductAttributes.Queries;

public record GetProductAttributeListQuery(QueryModel QueryModel) : IRequest<ActionResponse<PagedResponse<ProductAttributeDto>>>;

public class GetProductAttributeListQueryHandler : IRequestHandler<GetProductAttributeListQuery, ActionResponse<PagedResponse<ProductAttributeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductAttributeListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<PagedResponse<ProductAttributeDto>>> Handle(GetProductAttributeListQuery request, CancellationToken cancellationToken)
    {
        var pagedAttributes = await _unitOfWork.ProductAttributeRepository.GetPagedAsync(request.QueryModel);
        return ActionResponse<PagedResponse<ProductAttributeDto>>.Success(pagedAttributes);
    }
}
